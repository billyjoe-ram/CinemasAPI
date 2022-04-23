using System.ComponentModel.DataAnnotations;

namespace CinemasAPI.Data.Dtos.Gerente
{
    public class UpdateGerenteDto
    {
        [Required(ErrorMessage = "Nome do Gerente é obrigatório")]
        [StringLength(64)]
        public string Nome { get; set; }
    }
}
