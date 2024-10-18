namespace biblioteca.Model
{
    public class Reservas
    {
        public int Id { get; set; }

        public DateOnly DataReserva { get; set; }

        public int FkMembro { get; set; }

        public int FkLivro { get; set; }
    }
}
