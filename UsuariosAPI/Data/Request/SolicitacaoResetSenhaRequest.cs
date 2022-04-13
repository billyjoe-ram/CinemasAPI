using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Request
{
    public class SolicitacaoResetSenhaRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
