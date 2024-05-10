using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista
{
    internal class Revista : EntidadeBase
    {
        public string Titulo { get; set; }
        public string Numero { get; set; }
        public int Ano { get; set; }
        public Caixa Caixa { get; set; }

        public Revista(
            string titulo,
            string numero,
            int ano,
            Caixa caixa
        )
        {
            Titulo = titulo;
            Numero = numero;
            Ano = ano;
            Caixa = caixa;
        }

        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();

            if (string.IsNullOrEmpty(Titulo.Trim()))
                erros.Add("O campo \"titulo\" é obrigatório");

            if (string.IsNullOrEmpty(Numero.Trim()) || !int.TryParse(Numero, out _))
                erros.Add("O campo \"numero\" é obrigatório e deve ser um número válido");

            if (Caixa == null)
                erros.Add("O campo \"caixa\" é obrigatório");

            int anoAtual = DateTime.Now.Year;

            if (Ano > anoAtual)
                erros.Add("O campo \"ano\" não pode ser maior que o ano atual");

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Revista novasInformacoes = (Revista)novoRegistro;

            this.Titulo = novasInformacoes.Titulo;
            this.Numero = novasInformacoes.Numero;
            this.Ano = novasInformacoes.Ano;
            this.Caixa = novasInformacoes.Caixa;
        }
    }
}