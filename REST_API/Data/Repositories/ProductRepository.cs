using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using REST_API.Interfaces;
using REST_API.Models;

namespace REST_API.Data.Repositores
{
    /// <summary>
    /// Product Repository
    /// </summary>
    /// <seealso cref="REST_API.Interfaces.IProductRepository" />
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationContext _context = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public ProductRepository(IOptions<DatabaseSettings> settings)
        {
            _context = new ApplicationContext(settings);

        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> Get()
        {
            try
            {
                return await _context.Product.Find(_ => true).ToListAsync();
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
        public async Task<Product> Get(string value)
        {
            try
            {
                return await _context.Product
                                .Find(Builders<Product>.Filter.Eq("ProductCode", value))
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
        public async Task<Product> Create(Product entity)
        {
            try
            {
                entity.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                await _context.Product.InsertOneAsync(entity);
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
        public async Task<bool> Update(Product entity)
        {
            try
            {
                ReplaceOneResult actionResult = null;
                if (entity != null && entity.ProductCode != null)
                {
                    var loadedEntity = await Get(entity.ProductCode);

                    if (loadedEntity != null && loadedEntity.Id != null)
                    {
                        entity.Id = loadedEntity.Id;
                        actionResult = await _context.Product.
                            ReplaceOneAsync(
                                Builders<Product>.Filter.Eq("ProductCode", entity.ProductCode),
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
                DeleteResult actionResult = await _context.Product.DeleteOneAsync(
                     Builders<Product>.Filter.Eq("ProductCode", value));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch
            {
                return false;
            }
        }

    }
}
