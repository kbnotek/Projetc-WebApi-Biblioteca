using biblioteca.Model;

namespace biblioteca.Repositorio
{
    public interface ICategoria
    {
        public void Add(Categorias categoria);
        public List<Categorias> GetAll();
        public Categorias GetById(int id);
        void Update(Categorias categoria);
        void Delete(int id);
    }
}
