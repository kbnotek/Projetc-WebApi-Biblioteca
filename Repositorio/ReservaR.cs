using biblioteca.Model;
using biblioteca.ORM;

namespace biblioteca.Repositorio
{
    public class ReservaR : IReserva
    {
        private BdBibliotecaContext _context;

        public ReservaR(BdBibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Reservas reserva)
        {
            var tbreserva = new TbReserva()
            {
                DataReserva = reserva.DataReserva,
                FkMembro = reserva.FkMembro,
                FkLivro = reserva.FkLivro
            };

            _context.TbReservas.Add(tbreserva);

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var tbreserva = _context.TbReservas.FirstOrDefault(f => f.Id == id);

            if (tbreserva != null)
            {
                _context.TbReservas.Remove(tbreserva);

                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Reserva não encontrado.");
            }
        }

        public List<Reservas> GetAll()
        {
            List<Reservas> listFun = new List<Reservas>();

            var listTb = _context.TbReservas.ToList();

            foreach (var item in listTb)
            {
                var reserva = new Reservas
                {
                    Id = item.Id,
                    DataReserva = item.DataReserva,
                    FkMembro = item.FkMembro,
                    FkLivro = item.FkLivro

                };

                listFun.Add(reserva);
            }

            return listFun;
        }

        public Reservas GetById(int id)
        {

            var item = _context.TbReservas.FirstOrDefault(f => f.Id == id);

            if (item == null)
            {
                return null;
            }

            var reserva = new Reservas
            {
                Id = item.Id,
                DataReserva = item.DataReserva,
                FkMembro = item.FkMembro,
                FkLivro = item.FkLivro

            };

            return reserva;
        }

        public void Update(Reservas reserva)
        {

            var tbreserva = _context.TbReservas.FirstOrDefault(f => f.Id == reserva.Id);

            if (tbreserva != null)
            {
                tbreserva.DataReserva = reserva.DataReserva;
                tbreserva.FkMembro = reserva.FkMembro;
                tbreserva.FkLivro = reserva.FkLivro;


                _context.TbReservas.Update(tbreserva);

                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Reserva não encontrado.");
            }
        }
    }
}
