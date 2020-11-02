using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using WhatSappDatabase.MongoDB.Base;

namespace WhatSappWeb.Model
{
    [NomeDaColecao("FilaEnvio")]
    public class FilaEnvio
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        [BsonElement("Mensagem")]
        public virtual string Mensagem { get; set; }  
        
        [BsonElement("Telefone_Origem")]
        public virtual string TelefoneOrigem { get; set; }
        
        [BsonElement("Telefone_Destino")]
        public virtual string TelefoneDestino { get; set; }

        [BsonElement("Data_Envio")]
        public virtual DateTime? DataEnvio { get; set; }

        [BsonElement("Data_Cadastro")]
        public virtual DateTime? DataCadastro { get; set; }

        [BsonElement("Enviado")]
        public virtual bool Enviado { get; set; }

    }
}
