using System;
using WhatSappWeb.Model;

namespace WebApi.Autenticacao
{
    public class TokenCliente
    {
        public Cliente Cliente { get; set; }

        public Cliente Empresa { get; set; }

        public DateTime DataExpiracao { get; set; }
    }
}
