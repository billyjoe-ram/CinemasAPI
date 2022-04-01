using System.ComponentModel.DataAnnotations;

namespace CinemasAPI.Data.Dtos.Sessao
{
    public class ReadSessaoDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int FilmeId { get; set; }
        [Required]
        public int CinemaId { get; set; }
        public DateTime HorarioInicioSessao { get; set; }
        public DateTime HorarioFimSessao { get; set; }

    }
}
