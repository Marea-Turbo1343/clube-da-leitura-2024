using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    internal class TelaEmprestimo : TelaBase
    {
        private RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();
        private RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
        private RepositorioRevista repositorioRevista = new RepositorioRevista();

        public override char ApresentarMenu()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"        Gestão de {tipoEntidade}s        ");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            Console.WriteLine($"1 - Cadastrar {tipoEntidade}");
            Console.WriteLine($"2 - Editar {tipoEntidade}");
            Console.WriteLine($"3 - Visualizar {tipoEntidade}s");
            Console.WriteLine($"4 - Excluir {tipoEntidade}s");

            Console.WriteLine("S - Voltar");

            Console.WriteLine();

            Console.Write("Escolha uma das opções: ");
            char operacaoEscolhida = Convert.ToChar(Console.ReadLine());

            return operacaoEscolhida;
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine($"Visualizando {tipoEntidade}s...");
            }

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -10} | {2, -10} | {3, -15} | {4, -15}",
                "Id", "Amigo", "Revista", "DataEmprestimo", "DataDevolucao"
            );

            ArrayList emprestimosCadastrados = repositorioEmprestimo.SelecionarTodos();

            foreach (Emprestimo emprestimo in emprestimosCadastrados)
            {
                Console.WriteLine(
                    "{0, -10} | {1, -10} | {2, -10} | {3, -15} | {4, -15}",
                    emprestimo.Id, emprestimo.Amigo.Nome, emprestimo.Revista.Titulo, emprestimo.DataEmprestimo, emprestimo.DataDevolucao
                );
            }

            Console.ReadLine();
            Console.WriteLine();
        }

        protected override EntidadeBase ObterRegistro()
        {
            Console.Write("Digite o nome do amigo: ");
            string nomeAmigo = Console.ReadLine();

            Console.Write("Digite o título da revista: ");
            string tituloRevista = Console.ReadLine();

            Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarPorNome(nomeAmigo);
            Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarPorTitulo(tituloRevista);

            return new Emprestimo(amigoSelecionado, revistaSelecionada);
        }

        public void CadastrarEntidadeTeste()
        {
            Amigo amigo = new Amigo("Bobby Tables", "Pedro", "49 9999-9521", "Rua Z5");
            Revista revista = new Revista("Revista TCHOLA", 1, 2024, new Caixa("ABC123", "Verde", 1, "Revista TCHOLA"));

            repositorioAmigo.Cadastrar(amigo);
            repositorioRevista.Cadastrar(revista);

            Emprestimo emprestimo = new Emprestimo(amigo, revista);

            repositorioEmprestimo.Cadastrar(emprestimo);
        }
    }
}