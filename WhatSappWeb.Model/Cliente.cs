using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using WhatSappDatabase.MongoDB.Base;

namespace WhatSappWeb.Model
{
    [NomeDaColecao("cliente")]
    public class Cliente
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        [BsonElement("Nome")]
        public virtual string Nome { get; set; }

        [BsonElement("Email")]
        public virtual string Email { get; set; }    
        
        [BsonElement("Senha")]
        public virtual string Senha { get; set; }

        [BsonElement("Telefone")]
        public virtual string Telefone { get; set; }

        [BsonElement("Empresa")]
        public virtual bool Empresa { get; set; }

        [BsonElement("Data_Cadastro")]
        public virtual DateTime? DataCadastro { get; set; }

        [BsonElement("Token")]
        public virtual Token Token { get; set; }    
        
        [BsonElement("Codigo_Verificacao")]
        public virtual CodigoVerificacao CodigoVerificacao { get; set; }

    }
}
