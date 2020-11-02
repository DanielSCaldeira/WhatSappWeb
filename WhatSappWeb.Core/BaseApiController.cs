using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;

namespace WhatSappWeb.Core
{
    public class BaseControllerBase : ControllerBase
    {
        protected string Erro(ModelStateDictionary modelState)
        {
            StringBuilder msgErro = new StringBuilder();
            foreach (var model in modelState.Values)
            {
                foreach (ModelError error in model.Errors)
                {
                    msgErro.AppendLine($"{error.ErrorMessage}");
                }
            }

            return msgErro.ToString();
        }
    }
}
