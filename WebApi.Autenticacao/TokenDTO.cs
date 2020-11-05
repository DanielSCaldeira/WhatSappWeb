using System;

namespace WebApi.Autenticacao
{
    public class TokenDTO
    {
        public virtual TokenAtualDTO Token { get; set; }

        public virtual TokenRefrexDTO TokenRefrex { get; set; }
    }
    
    public class TokenRefrexDTO
    {
        public virtual string Conteudo { get; set; }

        public virtual DateTime? DataExpiracao { get; set; }
    }  
    
    public class TokenAtualDTO
    {
        public virtual string Conteudo { get; set; }

        public virtual DateTime? DataExpiracao { get; set; }
    }
}
