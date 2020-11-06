using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using WhatSappDatabase.MongoDB.Base;

namespace WhatSappWeb.Model
{
    [NomeDaColecao("Empresa")]
    public class Empresa
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        [BsonElement("Nome")]
        public virtual string Nome { get; set; }

        [BsonElement("Senha")]
        public virtual string Senha { get; set; }    
        
        [BsonElement("Url")]
        public virtual string Url { get; set; }

        [BsonElement("Telefone")]
        public virtual string Telefone { get; set; }

        [BsonElement("Email")]
        public virtual Email Email { get; set; }

        [BsonElement("Data_Cadastro")]
        public virtual DateTime? DataCadastro { get; set; }

        [BsonElement("Token")]
        public virtual TokenEmpresa Token { get; set; } 
        
        [BsonElement("Pessoa")]
        public virtual List<PessoaEmpresa> Pessoas { get; set; }

    }
}
