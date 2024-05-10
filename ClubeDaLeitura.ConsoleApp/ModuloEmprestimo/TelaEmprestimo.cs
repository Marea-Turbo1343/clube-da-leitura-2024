using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    internal class TelaEmprestimo : TelaBase
    {
        private RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();
        private RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
        private RepositorioRevista repositorioRevista = new RepositorioRevista();
        private RepositorioCaixa repositorioCaixa = new RepositorioCaixa();

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
            Console.WriteLine($"5 - Concluir {tipoEntidade}");

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
            Console.WriteLine("Amigos:");
            ArrayList amigos = repositorioAmigo.SelecionarTodos();
            foreach (Amigo amigo in amigos)
            {
                Console.WriteLine($"ID: {amigo.Id}, Nome: {amigo.Nome}");
            }

            Console.Write("Digite o ID do amigo: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine());

            Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarPorId(idAmigo);

            Console.WriteLine("Revistas disponíveis:");
            ArrayList revistas = repositorioRevista.SelecionarTodos();
            foreach (Revista revista in revistas)
            {
                Emprestimo emprestimo = (Emprestimo)repositorioEmprestimo.SelecionarPorRevista(revista);
                if (emprestimo == null || emprestimo.Concluido)
                {
                    Console.WriteLine($"ID: {revista.Id}, Título: {revista.Titulo}");
                }
            }

            Console.Write("Digite o ID da revista: ");
            int idRevista = Convert.ToInt32(Console.ReadLine());

            Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarPorId(idRevista);

            Emprestimo novoEmprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada);
            novoEmprestimo.DataEmprestimo = DateTime.Now;
            novoEmprestimo.DataDevolucao = DateTime.Now.AddDays(revistaSelecionada.Caixa.DiasEmprestimo);

            return novoEmprestimo;
        }


        public void ConcluirEmprestimo()
        {
            RepositorioReserva repositorioReserva = new RepositorioReserva();
            RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();

            Console.Write("Digite o ID do empréstimo que deseja concluir: ");
            int idEmprestimo = Convert.ToInt32(Console.ReadLine());

            Emprestimo emprestimo = (Emprestimo)repositorioEmprestimo.SelecionarPorId(idEmprestimo);

            if (emprestimo == null)
            {
                Console.WriteLine("Empréstimo não encontrado.");
                return;
            }

            emprestimo.Concluir(repositorioReserva, repositorioEmprestimo);

            if (emprestimo.Multa > 0)
            {
                Console.WriteLine($"O empréstimo foi concluído, mas há uma multa de {emprestimo.Multa} reais devido ao atraso na devolução.");
            }
            else
            {
                Console.WriteLine("O empréstimo foi concluído com sucesso.");
            }
        }

        public void CadastrarEntidadeTeste()
        {
            Amigo amigo = (Amigo)repositorioAmigo.SelecionarTodos()[0];
            Revista revista = (Revista)repositorioRevista.SelecionarTodos()[0];

            Emprestimo emprestimo = new Emprestimo(amigo, revista);

            repositorioEmprestimo.Cadastrar(emprestimo);
        }
    }
}