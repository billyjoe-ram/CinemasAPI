using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CinemasAPI.Models
{
    public class Endereco
    {
        [Key]
        [Required]
        public int Id { get; private set; }

        [Required]
        [StringLength(8)]
        public string CEP { get; private set; }
        [Required]
        [StringLength(64)]
        public string Logradouro { get; private set; }

        [Required]
        [StringLength(64)]
        public string Bairro { get; private set; }

        public int? Numero { get; private set; }

        [StringLength(64)]
        public string? Complemento { get; private set; }
        [JsonIgnore]
        public virtual Cinema Cinema { get; set; }

        public Endereco(string logradouro, string bairro, int numero, string complemento)
        {
            Logradouro = logradouro;
            Bairro = bairro;
            Numero = numero;
            Complemento = complemento;
        }

        public Endereco(string logradouro, string bairro, int numero)
        {
            Logradouro = logradouro;
            Bairro = bairro;
            Numero = numero;
        }

        public Endereco(string logradouro, string bairro)
        {
            Logradouro = logradouro;
            Bairro = bairro;
        }
    }
}
