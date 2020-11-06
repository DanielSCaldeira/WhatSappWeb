using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using WhatSappDatabase.MongoDB.Base;

namespace WhatSappWeb.Model
{
    [NomeDaColecao("Apps")]
    public class Apps
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        [BsonElement("Nome")]
        public virtual string Nome { get; set; }
    }
}
