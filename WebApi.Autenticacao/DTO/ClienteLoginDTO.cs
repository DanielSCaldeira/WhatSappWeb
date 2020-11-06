using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Autenticacao
{
    public class ClienteLoginDTO
    {

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "O campo {0} comprimento deve estar entre {2} e {1}.", MinimumLength = 13)]
        public string Senha { get; set; }
    }
}
