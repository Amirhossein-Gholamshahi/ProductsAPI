using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ProductsAPI.Entities
{
	public class Product
	{
		[BsonId] 
		[BsonRepresentation(BsonType.ObjectId)]
		[JsonIgnore]
		public string ProductId { get; set; }

		[BsonElement("Name")]
		public string Name { get; set; }

		[BsonElement("Price")]
		public decimal Price { get; set; }

		[BsonElement("Quantity")]
		public int Quantity { get; set; }
	}
}
