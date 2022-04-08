using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Request
{
    public class AtivaContaRequest
    {
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public string CodigoAtivacao { get; set; }
    }
}
