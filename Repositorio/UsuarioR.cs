using biblioteca.ORM;

namespace biblioteca.Repositorio
{
    public class UsuarioR
    {
        private readonly BdBibliotecaContext _context;

        public UsuarioR(BdBibliotecaContext context)
        {
            _context = context;
        }

        public TbUsuario GetByCredentials(string usuario, string senha)
        {
            // Aqui você deve usar a lógica de hash para comparar a senha
            return _context.TbUsuarios.FirstOrDefault(u => u.Usuario == usuario && u.Senha == senha);
        }

    }
}
