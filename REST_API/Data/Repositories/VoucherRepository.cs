using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using REST_API.Interfaces;
using REST_API.Models;

namespace REST_API.Data.Repositores
{
    /// <summary>
    /// Voucher Repository
    /// </summary>
    /// <seealso cref="REST_API.Interfaces.IVoucherRepository" />
    public class VoucherRepository : IVoucherRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationContext _context = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public VoucherRepository(IOptions<DatabaseSettings> settings)
        {
            _context = new ApplicationContext(settings);

        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Voucher>> Get()
        {
            try
            {
                return await _context.Voucher.Find(_ => true).ToListAsync();
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
        public async Task<Voucher> Get(string value)
        {
            try
            {
                return await _context.Voucher
                                .Find(Builders<Voucher>.Filter.Eq("VoucherId", value))
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
        public async Task<Voucher> Create(Voucher entity)
        {
            try
            {
                entity.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                await _context.Voucher.InsertOneAsync(entity);
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
        public async Task<bool> Update(Voucher entity)
        {
            try
            {
                ReplaceOneResult actionResult = null;
                if (entity != null && entity.VoucherId != null)
                {
                    var loadedEntity = await Get(entity.VoucherId);

                    if (loadedEntity != null && loadedEntity.Id != null)
                    {
                        entity.Id = loadedEntity.Id;
                        actionResult = await _context.Voucher.
                            ReplaceOneAsync(
                                Builders<Voucher>.Filter.Eq("VoucherId", entity.VoucherId),
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
                DeleteResult actionResult = await _context.Voucher.DeleteOneAsync(
                     Builders<Voucher>.Filter.Eq("VoucherId", value));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// Gets the users vouche.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Voucher>> GetUsersVoucher(string username, string state)
        {
            FilterDefinition<Voucher> filters;

            if (state != null){
                filters =
                    Builders<Voucher>.Filter.Eq(f => f.Username, username) &
                    Builders<Voucher>.Filter.Eq(f => f.State, state);
            }
            else
            {
                filters = Builders<Voucher>.Filter.Eq(f => f.Username, username);
            }

            return await _context.Voucher
                            .Find(filters)
                            .ToListAsync();
        }
    }
}
