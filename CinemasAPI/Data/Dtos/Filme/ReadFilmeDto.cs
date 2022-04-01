using CinemasAPI.Models;

namespace CinemasAPI.Data.Dtos.Filme
{
    public class ReadFilmeDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        public string Diretor { get; set; }

        public int DuracaoEmMinutos { get; set; }
        public string Genero { get; set; }
        public int ClassificacaoEtaria { get; set; }
    }
}
