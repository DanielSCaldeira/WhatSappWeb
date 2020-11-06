using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WhatSappWeb.Model
{
    public class TokenEmpresa
    {

        [BsonElement("Token")]
        public virtual string Token { get; set; }

        [BsonElement("Data_Expiracao")]
        public virtual DateTime DataExpiracao { get; set; }

    }
}
