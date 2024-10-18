using biblioteca.Model;

namespace biblioteca.Repositorio
{
    public interface IFuncionario
    {
        public void Add(Funcionarios funcionario);
        public List<Funcionarios> GetAll();
        public Funcionarios GetById(int id);
        void Update(Funcionarios funcionario);
        void Delete(int id);
    }
}
