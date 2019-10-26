using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace REST_API.Models
{
    /// <summary>
    /// Product
    /// </summary>
    public class Product
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
        /// Gets or sets the product code.
        /// </summary>
        /// <value>
        /// The product code.
        /// </value>
        [BsonRepresentation(BsonType.String), BsonElement(elementName: "ProductCode")]
        public string ProductCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>
        /// The name of the product.
        /// </value>
        [BsonRepresentation(BsonType.String), BsonElement(elementName: "ProductName")]
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [BsonRepresentation(BsonType.Double), BsonElement(elementName: "Price")]
        public double Price { get; set; }

    }
}
