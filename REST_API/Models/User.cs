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
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [BsonRepresentation(BsonType.String), BsonElement(elementName: "Username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [BsonRepresentation(BsonType.String), BsonElement(elementName: "Password")]
        public string Password { get; set; }


        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [BsonRepresentation(BsonType.String), BsonElement(elementName: "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the nif.
        /// </summary>
        /// <value>
        /// The nif.
        /// </value>
        [BsonRepresentation(BsonType.String), BsonElement(elementName: "Nif")]
        public string Nif { get; set; }

        /// <summary>
        /// Gets or sets the credit card number.
        /// </summary>
        /// <value>
        /// The credit card number.
        /// </value>
        [BsonRepresentation(BsonType.String), BsonElement(elementName: "CreditCardNumber")]
        public string CreditCardNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the credit card.
        /// </summary>
        /// <value>
        /// The type of the credit card.
        /// </value>
        [BsonRepresentation(BsonType.String), BsonElement(elementName: "CreditCardType")]
        public string CreditCardType { get; set; }

        /// <summary>
        /// Gets or sets the credit card expiration.
        /// </summary>
        /// <value>
        /// The credit card expiration.
        /// </value>
        [BsonRepresentation(BsonType.String), BsonElement(elementName: "CreditCardExpiration")]
        public string CreditCardExpiration { get; set; }
    }
}
