namespace biblioteca.Model
{
    public class Emprestimos
    {
        public int Id { get; set; }

        public DateOnly DataEmprestimo { get; set; }

        public DateOnly DataDevolucao { get; set; }

        public int FkMembro { get; set; }

        public int FkLivros { get; set; }
    }
}
