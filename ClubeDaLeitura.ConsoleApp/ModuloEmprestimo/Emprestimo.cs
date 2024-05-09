using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    internal class Emprestimo : EntidadeBase
    {
        public Amigo Amigo { get; set; }
        public Revista Revista { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public bool Concluido { get; set; }
        public decimal Multa { get; private set; }

        public Emprestimo(Amigo amigo, Revista revista)
        {
            Amigo = amigo;
            Revista = revista;
            DataEmprestimo = DateTime.Now;
            DataDevolucao = DataEmprestimo.AddDays(Revista.Caixa.DiasEmprestimo);
            Concluido = false;
        }

        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();

            if (Amigo == null)
                erros.Add("O campo \"Amigo\" é obrigatório");

            if (Revista == null)
                erros.Add("O campo \"Revista\" é obrigatório");

            return erros;
        }

        public void Concluir()
        {
            DateTime dataHoje = DateTime.Now;

            if (dataHoje > DataDevolucao)
            {
                TimeSpan diferenca = dataHoje - DataDevolucao;
                int diasDiferenca = diferenca.Days;
                CalcularMulta(diasDiferenca);
            }

            Concluido = true;
        }

        private void CalcularMulta(int diasAtraso)
        {
            decimal valorMultaPorDia = 1.0m;
            Multa = valorMultaPorDia * diasAtraso;
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Emprestimo novasInformacoes = (Emprestimo)novoRegistro;

            this.Amigo = novasInformacoes.Amigo;
            this.Revista = novasInformacoes.Revista;
            this.DataEmprestimo = novasInformacoes.DataEmprestimo;
            this.DataDevolucao = novasInformacoes.DataDevolucao;
            this.Concluido = novasInformacoes.Concluido;
        }
    }
}
