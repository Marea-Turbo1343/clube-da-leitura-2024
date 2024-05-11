using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista
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
                "Id", "Titulo", "Numero", "Ano", "Caixa"
            );

            ArrayList revistasCadastradas = repositorio.SelecionarTodos();

            foreach (Revista revista in revistasCadastradas)
            {
                if (revista == null)
                    continue;

                Console.WriteLine(
                    "{0, -10} | {1, -20} | {2, -20} | {3, -20} | {4, -20}",
                    revista.Id, revista.Titulo, revista.Numero, revista.Ano, revista.Caixa.Etiqueta
                );
            }

            Console.ReadLine();
            Console.WriteLine();
        }

        protected override EntidadeBase ObterRegistro()
        {
            Console.Write("Digite o titulo da revista: ");
            string titulo = Console.ReadLine();

            string numero;
            while (true)
            {
                Console.Write("Digite o número da revista: ");
                numero = Console.ReadLine();

                if (int.TryParse(numero, out _))
                    break;

                Console.WriteLine("\nNúmero inválido. Por favor, insira um número válido.");
                Console.ReadLine();
                Console.WriteLine();
            }

            int ano;
            while (true)
            {
                Console.Write("Digite o ano da revista: ");
                string anoStr = Console.ReadLine();

                if (int.TryParse(anoStr, out ano) && ano <= DateTime.Now.Year)
                    break;

                Console.WriteLine("\nAno inválido. Por favor, insira um ano válido que não esteja no futuro.");
                Console.ReadLine();
                Console.WriteLine();
            }

            telaCaixa.VisualizarRegistros(false);

            Console.Write("\nDigite o ID da caixa da revista: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine());

            Caixa caixa = (Caixa)repositorioCaixa.SelecionarPorId(idCaixa);

            return new Revista(titulo, numero, ano, caixa);
        }

        public void CadastrarEntidadeTeste()
        {
            Caixa caixa = (Caixa)repositorioCaixa.SelecionarTodos()[0];

            int ano = 2024;

            Revista revista = new Revista("Revista Love", "1", ano, caixa);

            repositorio.Cadastrar(revista);
        }
    }
}