using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista
{
    internal class TelaRevista : TelaBase
    {
        public TelaFornecedor telaFornecedor = null;
        public RepositorioFornecedor repositorioFornecedor = null;

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine("Visualizando Medicamentos...");
            }

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -20} | {3, -20}",
                "Id", "Nome", "Fornecedor", "Quantidade"
            );

            ArrayList medicamentosCadastrados = repositorio.SelecionarTodos();

            foreach (Medicamento medicamento in medicamentosCadastrados)
            {
                if (medicamento == null)
                    continue;

                Console.WriteLine(
                    "{0, -10} | {1, -20} | {2, -20} | {3, -20}",
                    medicamento.Id, medicamento.Nome, medicamento.Fornecedor.Nome, medicamento.Quantidade
                );
            }

            Console.ReadLine();
            Console.WriteLine();
        }

        protected override EntidadeBase ObterRegistro()
        {
            Console.Write("Digite o nome: ");
            string nome = Console.ReadLine();

            Console.Write("Digite a descrição: ");
            string descricao = Console.ReadLine();

            Console.Write("Digite o lote: ");
            string lote = Console.ReadLine();

            Console.Write("Digite a data de validade: ");
            DateTime dataValidade = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Digite a quantidade disponivel do medicamento: ");
            int quantidade = Convert.ToInt32(Console.ReadLine());

            telaFornecedor.VisualizarRegistros(false);

            Console.Write("Digite o ID do fornecedor do medicamento: ");
            int idFornecedor = Convert.ToInt32(Console.ReadLine());

            Fornecedor fornecedor = (Fornecedor)repositorioFornecedor.SelecionarPorId(idFornecedor);

            return new Medicamento(nome, descricao, lote, dataValidade, fornecedor, quantidade);
        }

        public void CadastrarEntidadeTeste()
        {
            Fornecedor fornecedor = (Fornecedor)repositorioFornecedor.SelecionarTodos()[0];

            DateTime dataValidade = new DateTime(2025, 06, 20);

            Medicamento medicamento = new Medicamento("Paracetamol", "10mg", "000012X", dataValidade, fornecedor, 10);

            repositorio.Cadastrar(medicamento);
        }
    }
}
