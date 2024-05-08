using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloMedicamento
{
    internal class TelaRevista : TelaBase
    {
        public TelaCaixa telaCaixa = null;
        public RepositorioCaixa repositorioCaixa = null;

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine("Visualizando Revistas...");
            }

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -20} | {3, -20} | {4, -20}",
                "Id", "Titulo", "Numero", "DataAno", "Caixa"
            );

            ArrayList revistasCadastradas = repositorio.SelecionarTodos();

            foreach (Revista revista in revistasCadastradas)
            {
                if (revista == null)
                    continue;

                Console.WriteLine(
                    "{0, -10} | {1, -20} | {2, -20} | {3, -20} | {4, -20}",
                    revista.Id, revista.Titulo, revista.Numero, revista.DataAno, revista.Caixa
                );
            }

            Console.ReadLine();
            Console.WriteLine();
        }

        protected override EntidadeBase ObterRegistro()
        {
            Console.Write("Digite o titulo da revista: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite o número da revista: ");
            string numero = Console.ReadLine();

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