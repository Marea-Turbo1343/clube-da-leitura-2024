using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista
{
    internal class Revista: EntidadeBase
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Lote { get; set; }
        public DateTime DataValidade { get; set; }
        public Caixa Caixa { get; set; }

        public Revista(
            string titulo,
            string numero,
            DateTime dataAno,
            Caixa caixa
        )
        {
            Nome = nome;
            Descricao = descricao;
            Lote = lote;
            DataValidade = dataValidade;
            Fornecedor = fornecedor;
            Quantidade = quantidade;
        }

        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();

            if (string.IsNullOrEmpty(Nome.Trim()))
                erros.Add("O campo \"nome\" é obrigatório");

            if (string.IsNullOrEmpty(Descricao.Trim()))
                erros.Add("O campo \"descrição\" é obrigatório");

            if (string.IsNullOrEmpty(Lote.Trim()))
                erros.Add("O campo \"lote\" é obrigatório");

            if (Fornecedor == null)
                erros.Add("O campo \"fornecedor\" é obrigatório");

            DateTime hoje = DateTime.Now.Date;

            if (DataValidade < hoje)
                erros.Add("O campo \"data de validade\" não pode ser menor que a data atual");

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase novoegistro)
        {
            Medicamento novasInformacoes = (Medicamento)novoegistro;

            this.Nome = novasInformacoes.Nome;
            this.Descricao = novasInformacoes.Descricao;
            this.Lote = novasInformacoes.Lote;
            this.DataValidade = novasInformacoes.DataValidade;
            this.Fornecedor = novasInformacoes.Fornecedor;
            this.Quantidade = novasInformacoes.Quantidade;
        }
    }
}