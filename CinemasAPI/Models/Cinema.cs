using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CinemasAPI.Models
{
    public class Cinema
    {
        [Key]
        [Required]
        public int Id { get; private set; }
        [Required(ErrorMessage = "Nome do cinema é obrigatório")]
        [StringLength(128)]
        public string Nome { get; private set; }
        public virtual Endereco Endereco { get; private set; }
        [Required(ErrorMessage = "Endereço do cinema é obrigatório")]
        public int EnderecoId { get; private set; }
        public virtual Gerente Gerente { get; private set; }
        [Required(ErrorMessage = "Gerente do cinema é obrigatório")]
        public int GerenteId { get; private set; }
        [JsonIgnore]
        public virtual List<Sessao> Sessoes { get; private set; }

        public Cinema(string nome)
        {
            Nome = nome;
        }
    }
}
