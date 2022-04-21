namespace CinemasAPI.Data.Dtos.Cinema
{
    internal class ReadCinemaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Models.Endereco Endereco { get; set; }
        public Models.Gerente Gerente { get; set; }
    }
}

