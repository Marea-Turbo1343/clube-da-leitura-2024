using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    internal class Emprestimo : EntidadeBase
    {

        public Revista Revista { get; set; }
        public Amigo Amigo { get; set; }

        public DateTime Data { get; set; }

        public DateTime DataDevolucao { get; set; }

        public bool Concluido { get; set; }

        public Emprestimo(Revista revistaSelecionada, Amigo amigoSelecionado)
        {
            Revista = revistaSelecionada;
            Amigo = amigoSelecionado;
            Data = DateTime.Now;
            DataDevolucao = Data.AddDays(Revista.Caixa.TempoEmprestimo); // 3
        }

        //public Emprestimo(Amigo amigo, Revista revista)
        //{
        //    Amigo = amigo;
        //
        //    Revista = revista;
        //
        //    Data = DateTime.Now;
        //
        //   DataDevolucao = Data.AddDays(Revista.Caixa.TempoEmprestimo); // 3
        //}

        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();

            if (Revista == null)
                erros.Add("A revista precisa ser preenchida");

            if (Amigo == null)
                erros.Add("O amigo precisa ser informado");

            return erros;
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

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Emprestimo novasInformacoes = (Emprestimo)novoRegistro;

            this.Revista = novasInformacoes.Revista;
            this.Amigo = novasInformacoes.Amigo;
            this.Data = novasInformacoes.Data;
        }
    }
}