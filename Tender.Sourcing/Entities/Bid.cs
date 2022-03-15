using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Tender.Sourcing.Entities
{
  public class Bid
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string TenderId { get; set; }
    public string  ProductId { get; set; }
    public string SellerUserName { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}
