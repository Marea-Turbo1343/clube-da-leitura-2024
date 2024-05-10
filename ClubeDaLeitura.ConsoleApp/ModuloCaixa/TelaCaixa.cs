using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa
{
    internal class TelaCaixa : TelaBase
    {
        private RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine("Visualizando Caixas...");
            }

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -10} | {3, -10}",
                "Id", "Etiqueta", "Cor", "Tipo"
            );

            ArrayList caixasCadastrados = repositorio.SelecionarTodos();

            foreach (Caixa caixa in caixasCadastrados)
            {
                if (caixa == null)
                    continue;

                Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -10} | {3, -10}",
                "Id", "Etiqueta", "Cor", "Tipo"
                );
            }

            Console.ReadLine();
            Console.WriteLine();
        }

        protected override EntidadeBase ObterRegistro()
        {
            Console.Write("Digite a etiqueta: ");
            string etiqueta = Console.ReadLine();

            Console.Write("Digite a cor: ");
            string cor = Console.ReadLine();

            Console.Write("Tipo da caixa (1 - 'Padrão' / 2 - 'Novidade'): ");
            int tipo = Convert.ToInt32(Console.ReadLine());

            return new Caixa(etiqueta, cor, tipo);
        }

        public void CadastrarEntidadeTeste()
        {
            Caixa caixa = new Caixa("Romance", "Vermelha", 2);

            repositorio.Cadastrar(caixa);
        }
    }
}