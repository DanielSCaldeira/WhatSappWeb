using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WhatSappWeb.Model
{
    public class TokenEmpresa
    {

        [BsonElement("Conteudo")]
        public virtual string Conteudo { get; set; }

        [BsonElement("Descricao")]
        public virtual string Descricao { get; set; }

        [BsonElement("Data_Expiracao")]
        public virtual DateTime? DataExpiracao { get; set; }  
        
        [BsonElement("Token_Refrex")]
        public virtual TokenRefrex TokenRefrex { get; set; }


    }
}
