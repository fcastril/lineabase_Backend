using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Common {
  public class BaseEntity {
    public BaseEntity() {
      DateCreation = DateTime.UtcNow;
      DateLastUpdate = null;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public DateTime DateCreation { get; set; }
    public DateTime? DateLastUpdate { get; set; }
  }
}
