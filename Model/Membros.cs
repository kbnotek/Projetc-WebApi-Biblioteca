namespace biblioteca.Model
{
    public class Membros
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Telefone { get; set; } = null!;

        public DateTime DataCadastro { get; set; }

        public string TipoMembro { get; set; } = null!;
    }
}
