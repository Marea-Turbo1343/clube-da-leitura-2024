using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    internal class TelaEmprestimo : TelaBase
    {
        private RepositorioEmprestimo repositorioEmprestimo;
        private RepositorioReserva repositorioReserva;
        public RepositorioAmigo repositorioAmigo;
        public RepositorioRevista repositorioRevista;

        public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioAmigo repositorioAmigo, RepositorioRevista repositorioRevista, RepositorioReserva repositorioReserva)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.repositorioReserva = repositorioReserva;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioRevista = repositorioRevista;
        }

        public override char ApresentarMenu()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"        Gestão de {tipoEntidade}s        ");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            Console.WriteLine($"1 - Cadastrar {tipoEntidade}");
            Console.WriteLine($"2 - Editar {tipoEntidade}");
            Console.WriteLine($"3 - Excluir {tipoEntidade}s");
            Console.WriteLine($"4 - Visualizar {tipoEntidade}s");
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
                "{0, -10} | {1, -15} | {2, -20} | {3, -20} | {4, -20}",
                "Id", "Amigo", "Revista", "Data do Emprestimo", "Data de Devolucao"
            );

            ArrayList emprestimosCadastrados = repositorioEmprestimo.SelecionarTodos();

            foreach (Emprestimo emprestimo in emprestimosCadastrados)
            {
                Console.WriteLine(
                    "{0, -10} | {1, -15} | {2, -20} | {3, -20} | {4, -20}",
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
            Amigo amigo1 = (Amigo)repositorioAmigo.SelecionarTodos()[0];
            Revista revista1 = (Revista)repositorioRevista.SelecionarTodos()[0];
            Emprestimo emprestimo1 = new Emprestimo(amigo1, revista1);
            repositorioEmprestimo.Cadastrar(emprestimo1);

            Amigo amigo2 = (Amigo)repositorioAmigo.SelecionarTodos()[2];
            Revista revista2 = (Revista)repositorioRevista.SelecionarTodos()[0];
            Emprestimo emprestimo2 = new Emprestimo(amigo2, revista2);
            emprestimo2.DataEmprestimo = DateTime.Now.AddDays(-10);
            emprestimo2.DataDevolucao = DateTime.Now.AddDays(-5);
            emprestimo2.Concluir(repositorioReserva, repositorioEmprestimo);
            repositorioEmprestimo.Cadastrar(emprestimo2);
        }
    }
}