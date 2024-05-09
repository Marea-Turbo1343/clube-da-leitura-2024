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
                "Id", "Titulo", "Numero", "DataAno", "Caixa"
            );

            ArrayList revistasCadastradas = repositorio.SelecionarTodos();

            foreach (Revista revista in revistasCadastradas)
            {
                if (revista == null)
                    continue;

                Console.WriteLine(
                    "{0, -10} | {1, -20} | {2, -20} | {3, -20} | {4, -20}",
                    revista.Id, revista.Titulo, revista.Numero, revista.DataAno, revista.Caixa.Etiqueta
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

            DateTime dataAno;
            while (true)
            {
                Console.Write("Digite o ano da revista: ");
                string dataAnoStr = Console.ReadLine();

                if (DateTime.TryParse(dataAnoStr, out dataAno) && dataAno <= DateTime.Now)
                    break;

                Console.WriteLine("Data inválida. Por favor, insira uma data válida que não esteja no futuro.");
            }

            telaCaixa.VisualizarRegistros(false);

            Console.Write("Digite o ID da caixa da revista: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine());

            Caixa caixa = (Caixa)repositorioCaixa.SelecionarPorId(idCaixa);

            return new Revista(titulo, numero, dataAno, caixa);
        }

        public void CadastrarEntidadeTeste()
        {
            Caixa caixa = (Caixa)repositorioCaixa.SelecionarTodos()[0];

            DateTime dataAno = new DateTime(2025, 06, 20);

            // Agora precisamos passar o tipo da caixa como um número
            Revista revista = new Revista("Revista Teste", "2", dataAno, caixa.Tipo, caixa);

            repositorio.Cadastrar(revista);
        }
    }
}