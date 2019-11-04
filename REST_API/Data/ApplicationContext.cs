using Microsoft.Extensions.Options;
using MongoDB.Driver;
using REST_API.Models;

namespace REST_API.Data
{
    public class ApplicationContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationContext"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public ApplicationContext(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                Database = client.GetDatabase(settings.Value.Database);
            }
        }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public IMongoDatabase Database { get; } = null;

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
                return Database.GetCollection<User>("User");
            }
        }

        /// <summary>
        /// Gets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public IMongoCollection<Product> Product
        {
            get
            {
                return Database.GetCollection<Product>("Product");
            }
        }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public IMongoCollection<Order> Order
        {
            get
            {
                return Database.GetCollection<Order>("Order");
            }
        }

        /// <summary>
        /// Gets the voucher.
        /// </summary>
        /// <value>
        /// The voucher.
        /// </value>
        public IMongoCollection<Voucher> Voucher
        {
            get
            {
                return Database.GetCollection<Voucher>("Voucher");
            }
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IMongoCollection<Config> Config
        {
            get
            {
                return Database.GetCollection<Config>("Config");
            }
        }
    }
}
