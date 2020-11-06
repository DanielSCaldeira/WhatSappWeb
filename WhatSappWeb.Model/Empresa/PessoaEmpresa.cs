using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using WhatSappDatabase.MongoDB.Base;

namespace WhatSappWeb.Model
{
    public class PessoaEmpresa
    {

        [BsonElement("Chave")]
        public virtual string Chave { get; set; }

        [BsonElement("Data_Cadastro")]
        public virtual DateTime DataCadastro { get; set; }

    }
}
