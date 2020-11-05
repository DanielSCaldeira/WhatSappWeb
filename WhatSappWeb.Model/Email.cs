using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WhatSappWeb.Model
{
    public class Email
    {

        [BsonElement("Endereco")]
        public virtual string Endereco { get; set; }


        [BsonElement("Confirmado")]
        public virtual bool Confirmado { get; set; }

    }
}
