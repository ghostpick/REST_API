using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace REST_API.Models
{
    /// <summary>
    /// Voucher
    /// </summary>
    public class Voucher
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
        [BsonRepresentation(BsonType.ObjectId), BsonElement(elementName: "VoucherId")]
        public string VoucherId { get; set; }

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
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        [BsonRepresentation(BsonType.Double), BsonElement(elementName: "Discount")]
        public double Discount { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [BsonRepresentation(BsonType.String), BsonElement(elementName: "State")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        /// <value>
        /// The rate.
        /// </value>
        [BsonRepresentation(BsonType.Double), BsonElement(elementName: "Rate")]
        public double Rate { get; set; }
    }
}
