using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloMedicamento
{
    internal class Reserva : EntidadeBase
    {
        public string Titulo { get; set; }
        public string Numero { get; set; }
        public DateTime DataAno { get; set; }
        public Caixa Caixa { get; set; }

        public Reserva(
            string nome,
            string descricao,
            string lote,
            DateTime dataValidade,
            Fornecedor fornecedor,
            int quantidade
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