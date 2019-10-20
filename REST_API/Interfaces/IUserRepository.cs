using REST_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace REST_API.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<User>> Get();

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<User> Get(string id);

        /// <summary>
        /// Creates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task Create(User user);

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<bool> Update(string id, User user);

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<bool> Remove(string id);
    }
}
