using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    public class Emprestimo
    {
        public Amigo Amigo { get; set; }

        public Revista Revista { get; set; }

        public DateTime Data { get; set; }

        public DateTime DataDevolucao { get; set; }

        public bool Concluido { get; set; }

        public Emprestimo()
        {
            Data = DateTime.Now;

            DataDevolucao = Data.AddDays(Revista.Caixa.TempoEmprestimo); // 3
        }

        public Emprestimo(Amigo amigo, Revista revista)
        {
            Amigo = amigo;

            Revista = revista;

            Data = DateTime.Now;

            DataDevolucao = Data.AddDays(Revista.Caixa.TempoEmprestimo); // 3
        }

        public void Concluir()
        {
            DateTime dataHoje = DateTime.Now;

            TimeSpan diferenca = dataHoje - DataDevolucao;
            int diasDiferenca = diferenca.Days;

            if (dataHoje > DataDevolucao)
            {
                CalcularMulta(diasDiferenca);
            }

            Concluido = true;
        }

        public void CalcularMulta(int valorMulta)
        {
            decimal multa = valorMulta * diasDiferenca;

            return;
        }
    }
}