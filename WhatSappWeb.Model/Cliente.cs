using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using WhatSappDatabase.MongoDB.Base;

namespace WhatSappWeb.Model
{
    [NomeDaColecao("cliente")]
    public class Cliente
    {
        public Cliente()
        {
            //Preferencias = new Preferencias();
        }

        [BsonId]
        [BsonElement("id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        [BsonElement("nome")]
        public virtual string Nome { get; set; }

        [BsonElement("email")]
        public virtual string Email { get; set; }

        [BsonElement("telefone")]
        public virtual string Telefone { get; set; }

        [BsonElement("dataCadastro")]
        public virtual DateTime? DataCadastro { get; set; }

        [BsonElement("token")]
        public virtual Token Token { get; set; }

        [BsonElement("empresa")]
        public virtual bool Empresa { get; set; }

    }
}
