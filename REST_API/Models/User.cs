using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace REST_API.Models
{
    /// <summary>
    /// User
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the usermane.
        /// </summary>
        /// <value>
        /// The usermane.
        /// </value>
        public string Usermane { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        //[BsonRepresentation(BsonType.String)]
        //public string Name { get; set; }

        //[BsonRepresentation(BsonType.Int32)]
        //public string NIF { get; set; }

        //[BsonRepresentation(BsonType.Int32)]
        //public string CreditCardNumber { get; set; }

        //[BsonRepresentation(BsonType.Int32)]
        //public string CreditCardType { get; set; }

        //[BsonRepresentation(BsonType.Int32)]
        //public string CreditCardExpiration { get; set; }

        //[BsonRepresentation(BsonType.Int32)]
        //public string PublicKeyModulus { get; set; }

        //[BsonRepresentation(BsonType.Int32)]
        //public string PublicKeyExponent { get; set; }

        //public List<Purchase> Purchases { get; set; }

        //public List<Order> Orders { get; set; }

        //public List<Voucher> Vouchers { get; set; }
    }
}
