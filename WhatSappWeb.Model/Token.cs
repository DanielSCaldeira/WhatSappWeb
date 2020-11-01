using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WhatSappWeb.Model
{
    public class Token
    {

        [BsonElement("conteudo")]
        public virtual string conteudo { get; set; }

        [BsonElement("dataCadastro")]
        public virtual DateTime? DataExpiracao { get; set; }

    }
}
