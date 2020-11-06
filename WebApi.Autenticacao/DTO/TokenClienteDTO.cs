using System;
using WhatSappWeb.Model;

namespace WebApi.Autenticacao
{
    public class TokenClienteDTO
    {
        public ClienteDTO Cliente { get; set; }

        public ClienteDTO Empresa { get; set; }

        public DateTime DataExpiracao { get; set; }
    }
}
