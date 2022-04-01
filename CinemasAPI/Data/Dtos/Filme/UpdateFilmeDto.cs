using System.ComponentModel.DataAnnotations;

namespace CinemasAPI.Data.Dtos.Filme
{
    public class UpdateFilmeDto
    {
        [Required(ErrorMessage = "Título do Filme é obrigatório")]
        [StringLength(128)]
        public string Titulo { get; set; }
        [StringLength(128)]
        [Required(ErrorMessage = "Diretor do Filme é obrigatório")]
        public string Diretor { get; set; }
        [Required(ErrorMessage = "Duração do Filme é obrigatória")]
        [Range(1, 600)]
        public int DuracaoEmMinutos { get; set; }
        public int ClassificacaoEtaria { get; set; }
    }
}
