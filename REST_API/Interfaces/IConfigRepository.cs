using REST_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace REST_API.Interfaces
{
    public interface IConfigRepository
    {
        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <returns></returns>
        Task<Config> GetProperties();

        /// <summary>
        /// Updates the product keys asynchronous.
        /// </summary>
        /// <param name="modulus">The modulus.</param>
        /// <param name="exponent">The exponent.</param>
        /// <returns></returns>
        Task<Config> UpdateProductKeysAsync(string modulus, string exponent);

    }
}
