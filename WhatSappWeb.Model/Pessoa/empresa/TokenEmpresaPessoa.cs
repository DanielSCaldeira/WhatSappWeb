using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WhatSappWeb.Model
{
    public class TokenEmpresaPessoa
    {

        [BsonElement("Chave")]
        public virtual string Chave { get; set; }    
        
        [BsonElement("Chave_App")]
        public virtual string ChaveApp { get; set; }     
        
        [BsonElement("Code")]
        public virtual string Code { get; set; }  
        
        [BsonElement("Token")]
        public virtual string Token { get; set; }    
        
        [BsonElement("Refresh_Token")]
        public virtual string RefreshToken { get; set; }

        [BsonElement("Data_Criacao")]
        public virtual DateTime DataCriacao { get; set; }  
        
        [BsonElement("Data_Expiracao")]
        public virtual DateTime DataExpiracao { get; set; }

    }
}
