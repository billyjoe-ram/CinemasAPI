using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Request
{
    public class ResetSenhaRequest
    {
        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [Required]
        [Compare("Senha")]
        public string ConfirmaSenha { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]

        public string ResetSenhaToken { get; set; }
    }
}