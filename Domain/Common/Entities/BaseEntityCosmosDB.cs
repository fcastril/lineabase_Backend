using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Common
{

    public class BaseEntityCosmosDB : BaseGeneral
    {
        public BaseEntityCosmosDB(Guid id): base()
        {
            Id = id;
        }
        public BaseEntityCosmosDB(Guid id, DateTime dateCreation, string status, DateTime dateLastUpdate) : base(dateCreation, status, dateLastUpdate)
        {
            Id = id;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; private set; }
    }
}
