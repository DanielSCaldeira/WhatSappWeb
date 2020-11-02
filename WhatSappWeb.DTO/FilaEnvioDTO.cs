using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using WhatSappDatabase.MongoDB.Base;
using WhatSappWeb.Model;

namespace WhatSappWeb.DTO
{
    public class FilaEnvioDTO : BaseDTO<FilaEnvio, FilaEnvioDTO, FilaEnvio>
    {
        public FilaEnvioDTO()
         : base()
        {
        }

        public FilaEnvioDTO(FilaEnvio registro)
              : base(registro)
        {
            Id = registro.Id;
            Mensagem = registro.Mensagem;
            TelefoneDestino = registro.TelefoneDestino;
            TelefoneOrigem = registro.TelefoneOrigem;
            DataCadastro = registro.DataCadastro;
            DataEnvio = registro.DataEnvio;
            Enviado = registro.Enviado;
        }

        public override FilaEnvio Modelo(IMongoCollection<FilaEnvio> colecao)
        {

            FilaEnvio model = null;

            if (!string.IsNullOrEmpty(Id))
            {
                model = colecao.AsQueryable().Where(y => y.Id == Id).FirstOrDefault();
            }
            else
            {
                model = new FilaEnvio();
            }

            model.Mensagem = Mensagem;
            model.TelefoneDestino = TelefoneDestino;
            model.TelefoneOrigem = TelefoneOrigem;
            model.Enviado = Enviado;
            model.DataEnvio = DataEnvio;
            model.DataCadastro = model.DataCadastro ?? DateTime.Now;

            return model;
        }


        public string Id { get; set; }

        [Required]
        [System.ComponentModel.DisplayName("Mensagem")]
        [StringLength(1000, ErrorMessage = "O campo {0} comprimento deve estar entre {2} e {1}.", MinimumLength = 1)]
        public string Mensagem { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "O campo {0} comprimento deve estar entre {2} e {1}.", MinimumLength = 13)]
        public string TelefoneOrigem { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "O campo {0} comprimento deve estar entre {2} e {1}.", MinimumLength = 13)]
        public string TelefoneDestino { get; set; }

        public DateTime? DataEnvio { get; set; }

        public DateTime? DataCadastro { get; set; }

        public bool Enviado { get; set; }
    }
}
