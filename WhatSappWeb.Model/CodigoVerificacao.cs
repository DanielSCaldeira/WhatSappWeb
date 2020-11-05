using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WhatSappWeb.Model
{
    public class CodigoVerificacao
    {

        [BsonElement("Conteudo")]
        public virtual string Conteudo { get; set; }

        [BsonElement("Verificado")]
        public virtual bool Verificado { get; set; }

    }
}
