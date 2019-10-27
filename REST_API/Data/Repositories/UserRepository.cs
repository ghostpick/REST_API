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
    /// User Repository
    /// </summary>
    /// <seealso cref="REST_API.Interfaces.IUserRepository" />
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationContext _context = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public UserRepository(IOptions<DatabaseSettings> settings)
        {
            _context = new ApplicationContext(settings);

        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> Get()
        {
            try
            {
                return await _context.User.Find(_ => true).ToListAsync();
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
        public async Task<User> Get(string value)
        {
            try
            {
                return await _context.User
                                .Find(Builders<User>.Filter.Eq("Username", value))
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
        public async Task<User> Create(User entity)
        {
            try
            {
                entity.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                
                await _context.User.InsertOneAsync(entity);
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
        public async Task<bool> Update(User entity)
        {
            try
            {
                ReplaceOneResult actionResult = null;
                if (entity != null && entity.Username != null)
                {
                    var loadedEntity = await Get(entity.Username);

                    if (loadedEntity != null && loadedEntity.Id != null)
                    {
                        entity.Id = loadedEntity.Id;
                        actionResult = await _context.User.
                            ReplaceOneAsync(
                                Builders<User>.Filter.Eq("Username", entity.Username),
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
                DeleteResult actionResult = await _context.User.DeleteOneAsync(
                     Builders<User>.Filter.Eq("Username", value));

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
