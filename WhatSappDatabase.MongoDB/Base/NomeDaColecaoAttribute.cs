using System;
using System.Collections.Generic;
using System.Text;

namespace WhatSappDatabase.MongoDB.Base
{

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class NomeDaColecaoAttribute : Attribute
    {
        private string _nomeDaColecao;
        public NomeDaColecaoAttribute(string nomeDaColecao)
        {
            _nomeDaColecao = nomeDaColecao;
        }
        public string NomeDaColecao => _nomeDaColecao;
    }
}
