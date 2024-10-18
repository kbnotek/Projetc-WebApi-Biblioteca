using biblioteca.Model;

namespace biblioteca.Repositorio
{
    public interface ILivro
    {
        public void Add(Livros livro);
        public List<Livros> GetAll();
        public Livros GetById(int id);
        void Update(Livros livro);
        void Delete(int id);
    }
}
