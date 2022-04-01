using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CinemasAPI.Models
{
    public class Filme
    {
        [Key]
        public int Id { get; private set; }
        [Required(ErrorMessage = "Título do Filme é obrigatório")]
        [StringLength(128)]
        public string Titulo { get; private set; }
        [StringLength(128)]
        [Required(ErrorMessage = "Diretor do Filme é obrigatório")]
        public string Diretor { get; private set; }
        [Required(ErrorMessage = "Duração do Filme é obrigatória")]
        [Range(1, 600)]
        public int DuracaoEmMinutos { get; private set; }
        [StringLength(128)]
        [Required(ErrorMessage = "Gênero do Filme é obrigatório")]
        public string Genero { get; private set; }
        [Required(ErrorMessage = "Classificação Etária obrigatória")]
        public int ClassificacaoEtaria { get; set; }
        [JsonIgnore]
        public virtual List<Sessao> Sessoes { get; private set; }

        public Filme(string titulo, string diretor, int duracaoEmMinutos)
        {
            Titulo = titulo;
            Diretor = diretor;
            DuracaoEmMinutos = duracaoEmMinutos;
        }
    }
}
