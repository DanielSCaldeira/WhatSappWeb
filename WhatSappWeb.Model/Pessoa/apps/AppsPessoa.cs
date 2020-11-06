using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using WhatSappDatabase.MongoDB.Base;

namespace WhatSappWeb.Model
{
    public class AppsPessoa
    {
        [BsonElement("Chave")]
        public virtual string Chave { get; set; }

        [BsonElement("Compras")]
        public virtual List<Compras> Compras { get; set; }
    }
}
