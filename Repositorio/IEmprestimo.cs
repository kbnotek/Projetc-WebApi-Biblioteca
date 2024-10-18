using biblioteca.Model;

namespace biblioteca.Repositorio
{
    public interface IEmprestimo
    {
        public void Add(Emprestimos emprestimo);
        public List<Emprestimos> GetAll();
        public Emprestimos GetById(int id);
        void Update(Emprestimos categoria);
        void Delete(int id);
    }
}
