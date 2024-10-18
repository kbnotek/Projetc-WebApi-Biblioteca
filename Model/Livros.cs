namespace biblioteca.Model
{
    public class Livros
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public string Autor { get; set; } = null!;

        public DateTime AnoPublicacao { get; set; }

        public int FkCategoria { get; set; }
    }
}
