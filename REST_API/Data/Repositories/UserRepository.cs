using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using REST_API.Interfaces;
using REST_API.Models;
using System;
using REST_API.Data.Repositories;

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
                entity.Token = Guid.NewGuid().ToString().ToUpper().Substring(4, 24);

                await _context.User.InsertOneAsync(entity);

                InitialData init = new InitialData();
                await init.InitialVouchers(_context, entity.Username);
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

        /// <summary>
        /// Logins the asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<User> LoginAsync(string username, string password)
        {
            var filters =
                Builders<User>.Filter.Eq(u => u.Username, username) &
                Builders<User>.Filter.Eq(u => u.Password, password);

            try
            {
                var user = _context.User
                                .Find(filters)
                                .FirstOrDefaultAsync();

                if (user != null)
                {
                    var updateField = Builders<User>.Update.Set("Token", Guid.NewGuid().ToString().ToUpper().Substring(4, 24));
                    var updateResult = await _context.User.UpdateManyAsync(filters, updateField);

                    if (updateResult != null && updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
                    {
                        return await _context.User.Find(filters).FirstOrDefaultAsync();
                    }
                }

            }
            catch
            {
                return null;
            }

            return null;
        }
    }
}
