using System.ComponentModel.DataAnnotations;

namespace CinemasAPI.Models
{
    internal class Sessao
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public int FilmeId { get; private set; }
        public virtual Filme Filme { get; private set; }

        [Required]
        public int CinemaId { get; private set; }
        public virtual Cinema Cinema { get; private set; }
        [Required]
        public DateTime HorarioInicioSessao { get; private set; }

        public Sessao(int filmeId, int cinemaId)
        {
            FilmeId = filmeId;
            CinemaId = cinemaId;
        }
    }
}
