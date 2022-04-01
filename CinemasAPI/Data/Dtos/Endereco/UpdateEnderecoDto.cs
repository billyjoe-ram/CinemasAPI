using System.ComponentModel.DataAnnotations;

namespace CinemasAPI.Data.Dtos.Endereco
{
    public class UpdateEnderecoDto
    {
        [Required]
        [StringLength(8)]
        public string CEP { get; set; }
        [Required]
        [StringLength(64)]
        public string Logradouro { get; set; }

        [Required]
        [StringLength(64)]
        public string Bairro { get; set; }

        public int Numero { get; set; }

        [StringLength(64)]
        public string Complemento { get; set; }
    }
}
