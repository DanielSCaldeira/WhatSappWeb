using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using WhatSappDatabase.MongoDB.Base;

namespace WhatSappWeb.Model
{
    [NomeDaColecao("Pessoa")]
    public class Pessoa
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        [BsonElement("Nome")]
        public virtual string Nome { get; set; }

        [BsonElement("Email")]
        public virtual Email Email { get; set; }

        [BsonElement("Senha")]
        public virtual string Senha { get; set; }

        [BsonElement("Telefone")]
        public virtual string Telefone { get; set; }

        [BsonElement("Data_Cadastro")]
        public virtual DateTime? DataCadastro { get; set; }

        [BsonElement("Token")]
        public virtual TokenPessoa Token { get; set; }

        [BsonElement("Empresa")]
        [BsonIgnoreIfNull]
        public virtual List<EmpresaPessoa> Empresa { get; set; }

        [BsonElement("Apps")]
        [BsonIgnoreIfNull]
        public virtual List<AppsPessoa> Apps { get; set; }
    }
}
