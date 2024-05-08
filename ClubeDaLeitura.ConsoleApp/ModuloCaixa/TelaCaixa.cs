using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloFornecedor
{
    internal class TelaCaixa : TelaBase
    {
        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine("Visualizando Caixas...");
            }

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -10} | {2, -10} | {3, -15} | {4, -15}",
                "Id", "Etiqueta", "Cor", "DiasEmprestimo", "Revistas"
            );

            ArrayList caixasCadastrados = repositorio.SelecionarTodos();

            foreach (Caixa caixa in caixasCadastrados)
            {
                Console.WriteLine(
                    "{0, -10} | {1, -10} | {2, -10} | {3, -15} | {4, -15}",
                    caixa.Id, caixa.Etiqueta, caixa.Cor, caixa.DiasEmprestimo, caixa.Revistas
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

            Console.Write("Revista: 1 - 'Padrão' ou 2 - 'Novidade'? ");
            string diasEmprestimo = Console.ReadLine();

            Console.Write("Digite o nome da Revista: ");
            string revista = Console.ReadLine();

            return new Caixa(etiqueta, cor, diasEmprestimo, revista);
        }

        public void CadastrarEntidadeTeste()
        {
            Caixa caixa = new Caixa("ABC123", "Verde", "1", "Revista TCHOLA");

            repositorio.Cadastrar(caixa);
        }
    }
}