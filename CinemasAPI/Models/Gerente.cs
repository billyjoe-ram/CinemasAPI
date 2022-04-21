using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CinemasAPI.Models
{
    internal class Gerente
    {
        [Key]
        [Required]
        public int Id { get; private set; }

        [Required(ErrorMessage = "Nome do Gerente é obrigatório")]
        [StringLength(64)]
        public string Nome { get; private set; }
        [JsonIgnore]
        public virtual List<Cinema> Cinemas { get; private set; }

        public Gerente(string nome)
        {
            Nome = nome;
        }
    }
}
