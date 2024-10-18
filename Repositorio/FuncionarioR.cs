using biblioteca.Model;
using biblioteca.ORM;

namespace biblioteca.Repositorio
{
    public class FuncionarioR : IFuncionario
    {
        private BdBibliotecaContext _context;

        public FuncionarioR(BdBibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Funcionarios funcionario)
        {
            var tbfuncionario = new TbFuncionario()
            {
                Nome = funcionario.Nome,
                Email = funcionario.Email,
                Telefone = funcionario.Telefone,
                Cargo = funcionario.Cargo,
                
            };

            _context.TbFuncionarios.Add(tbfuncionario);

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var tbfuncionario = _context.TbFuncionarios.FirstOrDefault(f => f.Id == id);

            if (tbfuncionario != null)
            {
                _context.TbFuncionarios.Remove(tbfuncionario);

                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionario não encontrado.");
            }
        }

        public List<Funcionarios> GetAll()
        {
            List<Funcionarios> listFun = new List<Funcionarios>();

            var listTb = _context.TbFuncionarios.ToList();

            foreach (var item in listTb)
            {
                var funcionario = new Funcionarios
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Email = item.Email,
                    Telefone = item.Telefone,
                    Cargo = item.Cargo
                };

                listFun.Add(funcionario);
            }

            return listFun;
        }

        public Funcionarios GetById(int id)
        {

            var item = _context.TbFuncionarios.FirstOrDefault(f => f.Id == id);

            if (item == null)
            {
                return null;
            }

            var funcionario = new Funcionarios
            {
                Id = item.Id,
                Nome = item.Nome,
                Email = item.Email,
                Telefone = item.Telefone,
                Cargo = item.Cargo
            };

            return funcionario;
        }

        public void Update(Funcionarios funcionario)
        {

            var tbfuncionario = _context.TbFuncionarios.FirstOrDefault(f => f.Id == funcionario.Id);

            if (tbfuncionario != null)
            {
                tbfuncionario.Nome = funcionario.Nome;
                tbfuncionario.Email = funcionario.Email;
                tbfuncionario.Telefone = funcionario.Telefone;
                tbfuncionario.Cargo = funcionario.Cargo;

                _context.TbFuncionarios.Update(tbfuncionario);

                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionario não encontrado.");
            }
        }
    }
}

