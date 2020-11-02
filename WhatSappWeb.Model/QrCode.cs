using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using WhatSappDatabase.MongoDB.Base;

namespace WhatSappWeb.Model
{
    [NomeDaColecao("QrCode")]
    public class QrCode
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        [BsonElement("Src")]
        public virtual string Src { get; set; }

        [BsonElement("Telefone_Origem")]
        public virtual string TelefoneOrigem { get; set; }

        [BsonElement("Data_Expiracao")]
        public virtual DateTime? DataExpiracao { get; set; }
    }
}
