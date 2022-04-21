using System.ComponentModel.DataAnnotations;

namespace CinemasAPI.Data.Dtos.Sessao
{
    internal class CreateSessaoDto
    {
        [Required]
        public int FilmeId { get; set; }
        [Required]
        public int CinemaId { get; set; }
        public DateTime HorarioInicioSessao { get; set; }
    }
}
