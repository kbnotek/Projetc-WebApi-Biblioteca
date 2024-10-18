using biblioteca.Model;

namespace biblioteca.Repositorio
{
    public interface IReserva
    {
        public void Add(Reservas reserva);
        public List<Reservas> GetAll();
        public Reservas GetById(int id);
        void Update(Reservas reserva);
        void Delete(int id);
    }
}
