using biblioteca.Model;

namespace biblioteca.Repositorio
{
    public interface IMembro
    {
        public void Add(Membros membro);
        public List<Membros> GetAll();
        public Membros GetById(int id);
        void Update(Membros membro);
        void Delete(int id);
    }
}
