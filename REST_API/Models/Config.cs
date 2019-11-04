using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace REST_API.Models
{
    /// <summary>
    /// Config
    /// </summary>
    public class Config
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
        /// Gets or sets the product public key modulus.
        /// </summary>
        /// <value>
        /// The product public key modulus.
        /// </value>
        [BsonRepresentation(BsonType.String), BsonElement(elementName: "ProductPublicKeyModulus")]
        public string ProductPublicKeyModulus { get; set; }

        /// <summary>
        /// Gets or sets the product public key exponent.
        /// </summary>
        /// <value>
        /// The product public key exponent.
        /// </value>
        [BsonRepresentation(BsonType.String), BsonElement(elementName: "ProductPublicKeyExponent")]
        public string ProductPublicKeyExponent { get; set; }
    }
}
