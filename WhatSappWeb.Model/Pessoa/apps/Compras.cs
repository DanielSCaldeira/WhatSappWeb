using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using WhatSappDatabase.MongoDB.Base;

namespace WhatSappWeb.Model
{
    public class Compras
    {
        [BsonElement("Valor")]
        public virtual string Valor { get; set; }

        [BsonElement("Data_Criacao")]
        public virtual DateTime DataCriacao { get; set; }

        [BsonElement("Data_Expiracao")]
        public virtual DateTime DataExpiracao { get; set; }
    }
}
