using System.ComponentModel.DataAnnotations;

namespace CinemasAPI.Data.Dtos.Cinema
{
    internal class UpdateCinemaDto
    {
        [Required(ErrorMessage = "Nome do Cinema é obrigatório")]
        [StringLength(128)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Endereço Obrigatório")]
        public int EnderecoId { get; set; }
        [Required(ErrorMessage = "Gerente Obrigatório")]
        public int GerenteId { get; set; }
    }
}
