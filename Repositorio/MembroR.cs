using biblioteca.Model;
using biblioteca.ORM;

namespace biblioteca.Repositorio
{
    public class MembroR : IMembro
    {
        private BdBibliotecaContext _context;

        public MembroR(BdBibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Membros membro)
        {
            var tbmembro = new TbMembro()
            {
                Nome = membro.Nome,
                Email = membro.Email,
                Telefone = membro.Telefone,
                DataCadastro = membro.DataCadastro,
                TipoMembro = membro.TipoMembro
            };

            _context.TbMembros.Add(tbmembro);

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var tbmembro = _context.TbMembros.FirstOrDefault(f => f.Id == id);

            if (tbmembro == null)
            {
                throw new Exception("Membro não encontrado.");
            }

            // Verificar se o membro está referenciado em outras tabelas
            var tbresevar = _context.TbReservas.Any(f => f.FkMembro == id);
            var tbemprestimo = _context.TbEmprestimos.Any(f => f.FkMembro == id); // Verifique outra tabela, se necessário

            if (tbresevar || tbemprestimo)
            {
                throw new Exception("Não é possível excluir o membro. Ele está referenciado em reservas ou outra tabela.");
            }

            _context.TbMembros.Remove(tbmembro);
            _context.SaveChanges();
        }

        public List<Membros> GetAll()
        {
            List<Membros> listFun = new List<Membros>();

            var listTb = _context.TbMembros.ToList();

            foreach (var item in listTb)
            {
                var membro = new Membros
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Email = item.Email,
                    Telefone = item.Telefone,
                    DataCadastro = item.DataCadastro,
                    TipoMembro = item.TipoMembro
                };

                listFun.Add(membro);
            }

            return listFun;
        }

        public Membros GetById(int id)
        {

            var item = _context.TbMembros.FirstOrDefault(f => f.Id == id);

            if (item == null)
            {
                return null;
            }

            var membro= new Membros
            {
                Id = item.Id,
                Nome = item.Nome,
                Email = item.Email,
                Telefone = item.Telefone,
                DataCadastro = item.DataCadastro,
                TipoMembro = item.TipoMembro
            };

            return membro;
        }

        public void Update(Membros membro)
        {

            var tbmembro = _context.TbMembros.FirstOrDefault(f => f.Id == membro.Id);

            if (tbmembro != null)
            {
                tbmembro.Nome = membro.Nome;
                tbmembro.Email= membro.Email;
                tbmembro.Telefone = membro.Telefone;
                tbmembro.DataCadastro = membro.DataCadastro;
                tbmembro.TipoMembro = membro.TipoMembro;

                _context.TbMembros.Update(tbmembro);

                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Membro não encontrado.");
            }
        }
    }
}
