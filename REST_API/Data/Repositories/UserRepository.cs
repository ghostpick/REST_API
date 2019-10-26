using Microsoft.Extensions.Options;
using MongoDB.Driver;
using REST_API.Interfaces;
using REST_API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace REST_API.Data.Repositores
{
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public async Task<User> Get(string username)
        {
            var filter = Builders<User>.Filter.Eq("Username", username);

            try
            {
                return await _context.User
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public async Task Create(User user)
        {
            try
            {
                user.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                await _context.User.InsertOneAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public async Task<bool> Update(string id, User user)
        {
            try
            {
                ReplaceOneResult actionResult =
                    await _context
                        .User
                        .ReplaceOneAsync(n =>
                            n.Id.Equals(id), 
                            user,
                            new UpdateOptions { IsUpsert = true });

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> Remove(string id)
        {
            try
            {
                DeleteResult actionResult = await _context.User.DeleteOneAsync(
                     Builders<User>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
