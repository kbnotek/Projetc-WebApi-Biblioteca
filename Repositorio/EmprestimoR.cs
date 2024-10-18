using biblioteca.Model;
using biblioteca.ORM;

namespace biblioteca.Repositorio
{
    public class EmprestimoR : IEmprestimo
    {
        private BdBibliotecaContext _context;

        public EmprestimoR(BdBibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Emprestimos emprestimo)
        {
            var tbemprestimo = new TbEmprestimo()
            {
                DataEmprestimo = emprestimo.DataEmprestimo,
                DataDevolucao = emprestimo.DataDevolucao,
                FkMembro = emprestimo.FkMembro,
                FkLivros = emprestimo.FkLivros,
            };

            _context.TbEmprestimos.Add(tbemprestimo);

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var tbemprestimo = _context.TbEmprestimos.FirstOrDefault(f => f.Id == id);

            if (tbemprestimo != null)
            {
                _context.TbEmprestimos.Remove(tbemprestimo);

                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Emprestimo não encontrado.");
            }
        }

        public List<Emprestimos> GetAll()
        {
            List<Emprestimos> listFun = new List<Emprestimos>();

            var listTb = _context.TbEmprestimos.ToList();

            foreach (var item in listTb)
            {
                var emprestimo = new Emprestimos
                {
                    Id = item.Id,
                    DataEmprestimo = item.DataEmprestimo,
                    DataDevolucao = item.DataDevolucao
                };

                listFun.Add(emprestimo);
            }

            return listFun;
        }

        public Emprestimos GetById(int id)
        {

            var item = _context.TbEmprestimos.FirstOrDefault(f => f.Id == id);

            if (item == null)
            {
                return null;
            }

            var emprestimo = new Emprestimos
            {
                Id = item.Id,
                DataEmprestimo = item.DataEmprestimo,
                DataDevolucao = item.DataDevolucao,
                FkMembro =  item.FkMembro,
                FkLivros = item.FkLivros
               
            };

            return emprestimo;
        }

        public void Update(Emprestimos emprestimo)
        {

            var tbemprestimo= _context.TbEmprestimos.FirstOrDefault(f => f.Id == emprestimo.Id);

            if (tbemprestimo != null)
            {
                tbemprestimo.DataEmprestimo = emprestimo.DataEmprestimo;
                tbemprestimo.DataDevolucao= emprestimo.DataDevolucao;
                tbemprestimo.FkMembro = emprestimo.FkMembro;
                tbemprestimo.FkLivros = emprestimo.FkLivros;

                _context.TbEmprestimos.Update(tbemprestimo);

                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Emprestimo não encontrado.");
            }
        }
    }
}
