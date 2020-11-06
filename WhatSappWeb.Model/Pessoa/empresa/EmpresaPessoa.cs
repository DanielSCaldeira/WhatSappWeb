using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WhatSappWeb.Model
{
    public class EmpresaPessoa
    {

        [BsonElement("Chave")]
        public virtual string Chave { get; set; }

        [BsonElement("Token_Empresa")]
        public virtual TokenEmpresaPessoa Token { get; set; }

    }
}
