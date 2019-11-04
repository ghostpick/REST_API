using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;
using REST_API.Interfaces;
using REST_API.Models;

namespace REST_API.Data.Repositores
{
    /// <summary>
    /// Config Repository
    /// </summary>
    /// <seealso cref="REST_API.Interfaces.IConfigRepository" />
    public class ConfigRepository : IConfigRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationContext _context = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigRepository"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public ConfigRepository(IOptions<DatabaseSettings> settings)
        {
            _context = new ApplicationContext(settings);

        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <returns></returns>
        public async Task<Config> GetProperties()
        {
            var x = await _context.Config.Find(_ => true).FirstOrDefaultAsync();

            return x;
        }

        /// <summary>
        /// Updates the property.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<Config> UpdateProductKeysAsync(string modulus, string exponent)
        {
            Config entity = null;
            try
            {
                ReplaceOneResult actionResult = null;
                if (modulus != null && exponent != null)
                {
                    entity = await GetProperties();
                    entity.ProductPublicKeyModulus = modulus;
                    entity.ProductPublicKeyExponent = exponent;

                    actionResult = await _context.Config.
                        ReplaceOneAsync(
                            Builders<Config>.Filter.Eq("Id", entity.Id),
                            entity,
                            new UpdateOptions { IsUpsert = true });
                }

                return entity;
            }
            catch
            {
                return entity;
            }
        }
    }
}
