using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace WebApi.Autenticacao
{
    public class Autenticacao : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }
    }
}
