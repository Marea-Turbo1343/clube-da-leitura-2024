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
        public RepositorioAmigo repositorioAmigo = RepositorioAmigo.Instancia;
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
            Console.WriteLine($"|        Gestão de {tipoEntidade}s        |");
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

            if (operacaoEscolhida == '4')
            {
                Console.Clear();

                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"        Visualizar {tipoEntidade}s        ");
                Console.WriteLine("----------------------------------------");

                Console.WriteLine();

                Console.WriteLine($"1 - Visualizar Emprestimos por Mês");
                Console.WriteLine($"2 - Visualizar todos os empréstimos");

                Console.WriteLine("S - Voltar");

                Console.WriteLine();

                Console.Write("Escolha uma das opções: ");
                char operacaoVisualizarEscolhida = Convert.ToChar(Console.ReadLine());

                if (operacaoVisualizarEscolhida == '1')
                {
                    VisualizarEmprestimosPorMes();
                }
                else if (operacaoVisualizarEscolhida == '2')
                {
                    VisualizarRegistros(true);
                }
            }

            return operacaoEscolhida;
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine($"Visualizando {tipoEntidade}s...");
            }

            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -20} | {3, -20} | {4, -20} | {5, -10}",
                "Id", "Amigo", "Revista", "Data do Emprestimo", "Data de Devolucao", "Concluído"
            );

            ArrayList emprestimosCadastrados = repositorioEmprestimo.SelecionarTodos();

            foreach (Emprestimo emprestimo in emprestimosCadastrados)
            {
                string concluido = emprestimo.Concluido ? "Sim" : "Não";
                Console.WriteLine(
                    "{0, -10} | {1, -15} | {2, -20} | {3, -20} | {4, -20} | {5, -10}",
                    emprestimo.Id, emprestimo.Amigo.Nome, emprestimo.Revista.Titulo, emprestimo.DataEmprestimo, emprestimo.DataDevolucao, concluido
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

            Console.Write("\nDigite o ID do amigo: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine());

            Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarPorId(idAmigo);

            ArrayList emprestimos = repositorioEmprestimo.SelecionarTodos();
            foreach (Emprestimo emprestimo in emprestimos)
            {
                if (emprestimo.Amigo.Id == amigoSelecionado.Id && !emprestimo.Concluido)
                {
                    Console.WriteLine("\nO amigo já possui um empréstimo ativo e não pode realizar outro até finalizar.");
                    Console.ReadLine();
                    Console.WriteLine();
                    return null;
                }
            }

            if (amigoSelecionado.Multa > 0)
            {
                Console.WriteLine("\nO amigo tem uma multa em aberto. Deseja quitar a multa?");
                Console.WriteLine("1 - Sim");
                Console.WriteLine("2 - Não");
                Console.Write("\nEscolha uma opção: ");
                int opcao = Convert.ToInt32(Console.ReadLine());

                if (opcao == 1)
                {
                    amigoSelecionado.Multa = 0;
                    Console.WriteLine("\nA multa foi quitada. Continuando com o empréstimo...");
                }
                else
                {
                    Console.WriteLine("\nO amigo precisa pagar a multa para realizar um novo empréstimo.");
                    Console.ReadLine();
                    Console.WriteLine();
                    return null;
                }
            }

            ArrayList reservas = repositorioReserva.SelecionarTodos();
            foreach (Reserva reserva in reservas)
            {
                if (reserva.Amigo.Id == amigoSelecionado.Id && !reserva.Expirada)
                {
                    Console.WriteLine("\nO amigo já possui uma reserva válida e não pode realizar um novo empréstimo.");
                    Console.ReadLine();
                    Console.WriteLine();
                    return null;
                }
            }

            Console.WriteLine("\nRevistas disponíveis:");
            ArrayList revistas = repositorioRevista.SelecionarTodos();
            ArrayList revistasDisponiveis = new ArrayList();
            foreach (Revista revista in revistas)
            {
                Emprestimo emprestimo = (Emprestimo)repositorioEmprestimo.SelecionarPorRevista(revista);
                if (emprestimo == null || emprestimo.Concluido)
                {
                    Console.WriteLine($"ID: {revista.Id}, Título: {revista.Titulo}");
                    revistasDisponiveis.Add(revista);
                }
            }

            if (revistasDisponiveis.Count == 0)
            {
                Console.WriteLine("\nNão há revistas disponíveis para empréstimo.");
                Console.ReadLine();
                Console.WriteLine();
                return null;
            }

            Console.Write("\nDigite o ID da revista: ");
            int idRevista = Convert.ToInt32(Console.ReadLine());

            Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarPorId(idRevista);

            if (!revistasDisponiveis.Contains(revistaSelecionada))
            {
                Console.WriteLine("\nA revista selecionada não está disponível para empréstimo.");
                Console.ReadLine();
                Console.WriteLine();
                return null;
            }

            return new Emprestimo(amigoSelecionado, revistaSelecionada, repositorioAmigo);
        }

        public override void Registrar()
        {
            ApresentarCabecalho();

            Console.WriteLine($"Cadastrando {tipoEntidade}...");

            Console.WriteLine();

            Emprestimo emprestimo = (Emprestimo)ObterRegistro();

            if (emprestimo == null)
            {
                Console.WriteLine("\nO empréstimo não pode ser registrado.");
                return;
            }

            ArrayList reservas = repositorioReserva.SelecionarTodos();
            foreach (Reserva reserva in reservas)
            {
                if (reserva.Amigo.Id == emprestimo.Amigo.Id && !reserva.Expirada)
                {
                    Console.WriteLine("\nO amigo já possui uma reserva válida e não pode realizar um novo empréstimo.");
                    return;
                }
            }

            InserirRegistro(emprestimo, repositorioEmprestimo);

            Console.ReadLine();
            Console.WriteLine();
        }

        public void ConcluirEmprestimo()
        {
            Console.Write("\nDigite o ID do empréstimo que deseja concluir: ");
            int idEmprestimo = Convert.ToInt32(Console.ReadLine());

            Emprestimo emprestimo = (Emprestimo)this.repositorioEmprestimo.SelecionarPorId(idEmprestimo);

            if (emprestimo == null)
            {
                Console.WriteLine("\nEmpréstimo não encontrado.");
                return;
            }

            emprestimo.Concluir(this.repositorioReserva, this.repositorioEmprestimo);

            ArrayList reservas = repositorioReserva.SelecionarTodos();
            foreach (Reserva reserva in reservas)
            {
                if (reserva.Amigo.Id == emprestimo.Amigo.Id && reserva.Revista.Id == emprestimo.Revista.Id && !reserva.Expirada)
                {
                    reserva.Expirada = true;

                    repositorioReserva.Editar(reserva.Id, reserva);
                    break;
                }
            }

            if (emprestimo.Multa > 0)
            {
                Console.WriteLine($"\nO empréstimo foi concluído, mas há uma multa de {emprestimo.Multa} reais devido ao atraso na devolução.");
            }
            else
            {
                Console.WriteLine("\nO empréstimo foi concluído com sucesso.");
            }
            Console.ReadLine();
            Console.WriteLine();
        }

        public void VisualizarEmprestimosPorMes()
        {
            Console.Write("Informe o mês (número): ");
            int mes = Convert.ToInt32(Console.ReadLine());

            Console.Write("Informe o ano: ");
            int ano = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -20} | {3, -20} | {4, -20} | {5, -10}",
                "Id", "Amigo", "Revista", "Data do Emprestimo", "Data de Devolucao", "Concluído"
            );

            foreach (Emprestimo emprestimo in repositorioEmprestimo.SelecionarTodos())
            {
                if (emprestimo.DataEmprestimo.Month == mes && emprestimo.DataEmprestimo.Year == ano)
                {
                    string concluido = emprestimo.Concluido ? "Sim" : "Não";
                    Console.WriteLine(
                        "{0, -10} | {1, -15} | {2, -20} | {3, -20} | {4, -20} | {5, -10}",
                        emprestimo.Id, emprestimo.Amigo.Nome, emprestimo.Revista.Titulo, emprestimo.DataEmprestimo, emprestimo.DataDevolucao, concluido
                    );
                }
            }
            Console.ReadLine();
            Console.WriteLine();
        }

        public void CadastrarEntidadeTeste()
        {
            Amigo amigo1 = (Amigo)repositorioAmigo.SelecionarTodos()[0];
            Revista revista1 = (Revista)repositorioRevista.SelecionarTodos()[0];
            Emprestimo emprestimo1 = new Emprestimo(amigo1, revista1, repositorioAmigo);
            emprestimo1.DataEmprestimo = DateTime.Now.AddDays(-1);
            repositorioEmprestimo.Cadastrar(emprestimo1);

            Amigo amigo2 = (Amigo)repositorioAmigo.SelecionarTodos()[2];
            Revista revista2 = (Revista)repositorioRevista.SelecionarTodos()[0];
            Emprestimo emprestimo2 = new Emprestimo(amigo2, revista2, repositorioAmigo);
            emprestimo2.DataEmprestimo = DateTime.Now.AddDays(-10);
            emprestimo2.DataDevolucao = DateTime.Now.AddDays(-5);
            repositorioEmprestimo.Cadastrar(emprestimo2);
            emprestimo2.Concluir(repositorioReserva, repositorioEmprestimo);
        }
    }
}