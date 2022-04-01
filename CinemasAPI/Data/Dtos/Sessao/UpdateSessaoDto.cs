using System.ComponentModel.DataAnnotations;

namespace CinemasAPI.Data.Dtos.Sessao
{
    public class UpdateSessaoDto
    {
        [Required]
        public int FilmeId { get; set; }
        [Required]
        public int CinemaId { get; set; }
        public DateTime HorarioInicioSessao { get; set; }
    }
}
