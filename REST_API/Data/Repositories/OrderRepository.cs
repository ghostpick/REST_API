using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using REST_API.Interfaces;
using REST_API.Models;
using MongoDB.Bson;

namespace REST_API.Data.Repositores
{
    /// <summary>
    /// Order Repository
    /// </summary>
    /// <seealso cref="REST_API.Interfaces.IOrderRepository" />
    public class OrderRepository : IOrderRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationContext _context = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepository"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public OrderRepository(IOptions<DatabaseSettings> settings)
        {
            _context = new ApplicationContext(settings);

        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Order>> Get()
        {
            try
            {
                return await _context.Order.Find(_ => true).ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public async Task<Order> Get(string value)
        {
            try
            {
                return await _context.Order
                                .Find(Builders<Order>.Filter.Eq("OrderId", value))
                                .FirstOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<Order> Create(Order entity)
        {
            try
            {
                entity.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                await _context.Order.InsertOneAsync(entity);
                return entity;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<bool> Update(Order entity)
        {
            try
            {
                ReplaceOneResult actionResult = null;
                if (entity != null && entity.OrderId != null)
                {
                    var loadedEntity = await Get(entity.OrderId);

                    if (loadedEntity != null && loadedEntity.Id != null)
                    {
                        entity.Id = loadedEntity.Id;
                        actionResult = await _context.Order.
                            ReplaceOneAsync(
                                Builders<Order>.Filter.Eq("OrderId", entity.OrderId),
                                entity,
                                new UpdateOptions { IsUpsert = true });

                    }
                }

                return
                    actionResult != null &&
                    actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Removes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public async Task<bool> Remove(string value)
        {
            try
            {
                DeleteResult actionResult = await _context.Order.DeleteManyAsync(
                     Builders<Order>.Filter.Eq("OrderId", value));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the orders of user asynchronous.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public async Task<List<OrderProducts>> GetOrdersOfUserAsync(string value, string state)
        {
            var options = new AggregateOptions()
            {
                AllowDiskUse = false
            };

            List<BsonDocument> filter = new List<BsonDocument>();
            // username
            filter.Add(
                new BsonDocument("$match", 
                new BsonDocument().Add("Username", value)));

            if(state != null)
            {
                filter.Add(
                    new BsonDocument("$match",
                    new BsonDocument().Add("State", state)));
            }

            // join products
            filter.Add(
                new BsonDocument("$lookup", new BsonDocument()
                        .Add("from", "Product")
                        .Add("localField", "ProductCode")
                        .Add("foreignField", "ProductCode")
                        .Add("as", "Product")));


            PipelineDefinition<BsonDocument, OrderProducts> pipeline = filter;

            var response = await _context.Database.GetCollection<BsonDocument>("Order")
                .AggregateAsync(pipeline, options);
            return await response.ToListAsync();
        }

        /// <summary>
        /// Creates the orders for multiple objects.
        /// </summary>
        /// <param name="orders">The orders.</param>
        /// <returns></returns>
        public async Task<string> CreateOrdersForMultipleObjects(List<Order> orders)
        {
            string orderId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            List<Order> ordersForCreate = new List<Order>();
            foreach (var order in orders)
            {
                Order orderForCreate = new Order();
                orderForCreate = order;
                orderForCreate.OrderId = orderId;
                orderForCreate.State = "In Progress";
                ordersForCreate.Add(orderForCreate);
            }

            await _context.Order.InsertManyAsync(orders);

            return orderId;
        }
    }
}
