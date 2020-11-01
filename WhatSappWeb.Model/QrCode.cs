using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using WhatSappDatabase.MongoDB.Base;

namespace WhatSappWeb.Model
{
    [NomeDaColecao("QrCode")]
    public class FilaEnvio
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        [BsonElement("Src")]
        public virtual string Telefone { get; set; }  
        
        [BsonElement("Telefone")]
        public virtual string Mensagem { get; set; }

        [BsonElement("Data_Enviada")]
        public virtual DateTime? DataEnviada { get; set; }

    }
}
