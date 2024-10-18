using biblioteca.Model;
using biblioteca.ORM;

namespace biblioteca.Repositorio
{
    public class CategoriaR : ICategoria
    {
        private BdBibliotecaContext _context;

        public CategoriaR(BdBibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Categorias categoria)
        {
            var tbcategoria = new TbCategorium()
            {
                Nome = categoria.Nome,
                Categoria = categoria.Categoria,               
            };
            
            _context.TbCategoria.Add(tbcategoria);
           
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var tbcategoria = _context.TbCategoria.FirstOrDefault(f => f.Id == id);

            if (tbcategoria == null)
            {
                throw new Exception("Categoria não encontrado.");
            }

            // Verificar se o membro está referenciado em outras tabelas
            var tblivros = _context.TbLivros.Any(f => f.FkCategoria == id);
           

            if (tblivros)
            {
                throw new Exception("Não é possível excluir o membro. Ele está referenciado em Livros");
            }

            _context.TbCategoria.Remove(tbcategoria);
            _context.SaveChanges();
        }

        public List<Categorias> GetAll()
        {
            List<Categorias> listFun = new List<Categorias>();

            var listTb = _context.TbCategoria.ToList();

            foreach (var item in listTb)
            {
                var categoria = new Categorias
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Categoria = item.Categoria,
                };

                listFun.Add(categoria);
            }

            return listFun;
        }

        public Categorias GetById(int id)
        {
          
            var item = _context.TbCategoria.FirstOrDefault(f => f.Id == id);
          
            if (item == null)
            {
                return null;
            }

            var categoria = new Categorias
            {
                Id = item.Id,
                Nome = item.Nome,
                Categoria = item.Categoria,
            };

            return categoria;
        }

        public void Update(Categorias categoria)
        {
           
            var tbcategoria = _context.TbCategoria.FirstOrDefault(f => f.Id == categoria.Id);

            if (tbcategoria != null)
            {
               tbcategoria.Nome = categoria.Nome;
               tbcategoria.Categoria = categoria.Categoria;

                _context.TbCategoria.Update(tbcategoria);
               
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Categoria não encontrado.");
            }
        }
    }
}
