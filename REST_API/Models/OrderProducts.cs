using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace REST_API.Models
{
    /// <summary>
    /// Order Products
    /// </summary>
    public class OrderProducts
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
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        [BsonRepresentation(BsonType.ObjectId), BsonElement(elementName: "OrderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [BsonRepresentation(BsonType.String), BsonElement(elementName: "Username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the product code.
        /// </summary>
        /// <value>
        /// The product code.
        /// </value>
        [BsonRepresentation(BsonType.String), BsonElement(elementName: "ProductCode")]
        public string ProductCode { get; set; }

        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        [BsonRepresentation(BsonType.String), BsonElement(elementName: "VoucherId")]
        public string VoucherId { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [BsonRepresentation(BsonType.Double), BsonElement(elementName: "Quantity")]
        public double Quantity { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [BsonRepresentation(BsonType.String), BsonElement(elementName: "State")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        [BsonElement(elementName: "Product")]
        public List<Product> Product { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        [BsonElement(elementName: "Voucher")]
        public List<Voucher> Voucher { get; set; }
    }
}
