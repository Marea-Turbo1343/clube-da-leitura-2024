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
                "Id", "Titulo", "Numero", "Ano", "Caixa" // Alterado de "DataAno" para "Ano"
            );

            ArrayList revistasCadastradas = repositorio.SelecionarTodos();

            foreach (Revista revista in revistasCadastradas)
            {
                if (revista == null)
                    continue;

                Console.WriteLine(
                    "{0, -10} | {1, -20} | {2, -20} | {3, -20} | {4, -20}",
                    revista.Id, revista.Titulo, revista.Numero, revista.Ano, revista.Caixa.Etiqueta // Alterado de revista.DataAno para revista.Ano
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

                Console.WriteLine("Número inválido. Por favor, insira um número válido.");
            }

            int ano;
            while (true)
            {
                Console.Write("Digite o ano da revista: ");
                string anoStr = Console.ReadLine();

                if (int.TryParse(anoStr, out ano) && ano <= DateTime.Now.Year)
                    break;

                Console.WriteLine("Ano inválido. Por favor, insira um ano válido que não esteja no futuro.");
            }

            telaCaixa.VisualizarRegistros(false);

            Console.Write("Digite o ID da caixa da revista: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine());

            Caixa caixa = (Caixa)repositorioCaixa.SelecionarPorId(idCaixa);

            return new Revista(titulo, numero, ano, caixa);
        }

        public void CadastrarEntidadeTeste()
        {
            Caixa caixa = (Caixa)repositorioCaixa.SelecionarTodos()[0];

            int ano = 2024;

            Revista revista = new Revista("Revista Teste", "2", ano, caixa);

            repositorio.Cadastrar(revista);
        }
    }
}