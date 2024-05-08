using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa
{
    internal class Caixa : EntidadeBase
    {
        public string Etiqueta { get; set; }
        public string Cor { get; set; }
        public string DiasEmprestimo { get; set; }
        public string Revistas { get; set; }


        public Caixa(string etiqueta, string cor, string diasEmprestimo, string revistas)
        {
            Etiqueta = etiqueta;
            Cor = cor;
            DiasEmprestimo = diasEmprestimo;
            Revistas = revistas;
        }

        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();

            if (string.IsNullOrEmpty(Etiqueta.Trim()))
                erros.Add("O campo \"etiqueta\" é obrigatório");

            if (string.IsNullOrEmpty(Cor.Trim()))
                erros.Add("O campo \"cor\" é obrigatório");

            if (string.IsNullOrEmpty(DiasEmprestimo.Trim()))
                erros.Add("O campo \"diasEmprestimo\" é obrigatório");

            if (string.IsNullOrEmpty(Revistas.Trim()))
                erros.Add("O campo \"revistas\" é obrigatório");

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Caixa novasInformacoes = (Caixa)novoRegistro;

            this.Etiqueta = novasInformacoes.Etiqueta;
            this.Cor = novasInformacoes.Cor;
            this.DiasEmprestimo = novasInformacoes.DiasEmprestimo;
            this.Revistas = novasInformacoes.Revistas;
        }
    }
}