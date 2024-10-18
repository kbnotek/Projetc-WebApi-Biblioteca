using biblioteca.Model;
using biblioteca.ORM;

namespace biblioteca.Repositorio
{
    public class LivroR : ILivro
    {
        private BdBibliotecaContext _context;

        public LivroR(BdBibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Livros livro)
        {
            var tblivro = new TbLivro()
            {
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                AnoPublicacao = livro.AnoPublicacao,
                FkCategoria = livro.FkCategoria
            };

            _context.TbLivros.Add(tblivro);

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var tblivro = _context.TbLivros.FirstOrDefault(f => f.Id == id);

            if (tblivro == null)
            {
                throw new Exception("Livro não encontrado.");
            }

            // Verificar se o membro está referenciado em outras tabelas
            var tbresevar = _context.TbReservas.Any(f => f.FkMembro == id);
            var tbemprestimo = _context.TbEmprestimos.Any(f => f.FkMembro == id); // Verifique outra tabela, se necessário

            if (tbresevar || tbemprestimo)
            {
                throw new Exception("Não é possível excluir o membro. Ele está referenciado em reservas ou Emprestimo.");
            }

            _context.TbLivros.Remove(tblivro);
            _context.SaveChanges();
        }

        public List<Livros> GetAll()
        {
            List<Livros> listFun = new List<Livros>();

            var listTb = _context.TbLivros.ToList();

            foreach (var item in listTb)
            {
                var livro = new Livros
                {
                    Id = item.Id,
                    Titulo=item.Titulo,
                    Autor=item.Autor,
                    AnoPublicacao = item.AnoPublicacao,
                    FkCategoria = item.FkCategoria
                  
                };

                listFun.Add(livro);
            }

            return listFun;
        }

        public Livros GetById(int id)
        {

            var item = _context.TbLivros.FirstOrDefault(f => f.Id == id);

            if (item == null)
            {
                return null;
            }

            var livro = new Livros
            {
                Id = item.Id,
                Titulo=item.Titulo,
                Autor=item.Autor,
                AnoPublicacao = item.AnoPublicacao,
                FkCategoria = item.FkCategoria
               
            };

            return livro;
        }

        public void Update(Livros livro)
        {

            var tblivro = _context.TbLivros.FirstOrDefault(f => f.Id == livro.Id);

            if (tblivro != null)
            {
                tblivro.Titulo = livro.Titulo;
                tblivro.Autor = livro.Autor;
                tblivro.AnoPublicacao = livro.AnoPublicacao;
                tblivro.FkCategoria = livro.FkCategoria;
               

                _context.TbLivros.Update(tblivro);

                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Livro não encontrado.");
            }
        }
    }
}
