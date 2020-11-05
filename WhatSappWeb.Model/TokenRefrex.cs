using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WhatSappWeb.Model
{
    public class TokenRefrex
    {

        [BsonElement("Conteudo")]
        public virtual string Conteudo { get; set; }

        [BsonElement("Descricao")]
        public virtual string Descricao { get; set; }

        [BsonElement("Data_Expiracao")]
        public virtual DateTime? DataExpiracao { get; set; }
    }
}
