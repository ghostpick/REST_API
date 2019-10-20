using Microsoft.Extensions.Options;
using MongoDB.Driver;
using REST_API.Models;

namespace REST_API.Data
{
    public class ApplicationContext
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly IMongoDatabase _database = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationContext"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public ApplicationContext(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public IMongoCollection<User> User
        {
            get
            {
                return _database.GetCollection<User>("User");
            }
        }
    }
}
