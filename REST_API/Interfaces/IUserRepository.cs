using REST_API.Models;
using System.Threading.Tasks;

namespace REST_API.Interfaces
{
    public interface IUserRepository : IBaseEntity<User>
    {
        /// <summary>
        /// Logins the asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<User> LoginAsync(string username, string password);
    }
}
