using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva
{
    internal class TelaReserva : TelaBase
    {
        private RepositorioReserva repositorioReserva;
        public RepositorioAmigo repositorioAmigo;
        public RepositorioRevista repositorioRevista;
        public RepositorioEmprestimo repositorioEmprestimo;

        public TelaReserva(RepositorioReserva repositorioReserva, RepositorioAmigo repositorioAmigo, RepositorioRevista repositorioRevista, RepositorioEmprestimo repositorioEmprestimo)
        {
            this.repositorioReserva = repositorioReserva;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioRevista = repositorioRevista;
            this.repositorioEmprestimo = repositorioEmprestimo;
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
                "{0, -10} | {1, -25} | {2, -25} | {3, -25} | {4, -25}",
                "Id", "Amigo", "Revista", "DataReserva", "Expirada"
            );

            ArrayList reservasCadastradas = repositorioReserva.SelecionarTodos();

            foreach (Reserva reserva in reservasCadastradas)
            {
                string statusReserva;
                if (reserva.Expirada)
                {
                    Emprestimo emprestimo = (Emprestimo)repositorioEmprestimo.SelecionarPorRevista(reserva.Revista);
                    if (emprestimo != null && emprestimo.Amigo.Id == reserva.Amigo.Id && !emprestimo.Concluido)
                    {
                        statusReserva = "Empréstimo realizado com sucesso";
                    }
                    else
                    {
                        statusReserva = "Reserva Expirada";
                    }
                }
                else
                {
                    statusReserva = "Reserva válida";
                }

                Console.WriteLine(
                    "{0, -10} | {1, -25} | {2, -25} | {3, -25} | {4, -25}",
                    reserva.Id, reserva.Amigo.Nome, reserva.Revista.Titulo, reserva.DataReserva, statusReserva
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

            Console.WriteLine("\nRevistas disponíveis:");
            ArrayList revistas = repositorioRevista.SelecionarTodos();
            foreach (Revista revista in revistas)
            {
                Emprestimo emprestimo = (Emprestimo)repositorioEmprestimo.SelecionarPorRevista(revista);
                if (emprestimo == null || emprestimo.Concluido)
                {
                    Console.WriteLine($"ID: {revista.Id}, Título: {revista.Titulo}");
                }
            }

            Console.Write("\nDigite o ID da revista: ");
            int idRevista = Convert.ToInt32(Console.ReadLine());

            Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarPorId(idRevista);

            ArrayList reservas = repositorioReserva.SelecionarTodos();
            foreach (Reserva reserva in reservas)
            {
                if (reserva.Revista.Id == revistaSelecionada.Id && !reserva.Expirada)
                {
                    Console.WriteLine("\nEsta revista já foi reservada. Por favor, escolha outra revista.");
                    return null;
                }
            }

            return new Reserva(amigoSelecionado, revistaSelecionada);
        }

        public void CadastrarEntidadeTeste()
        {
            Amigo amigo = (Amigo)repositorioAmigo.SelecionarTodos()[1];
            Revista revista = (Revista)repositorioRevista.SelecionarTodos()[0];

            Reserva reserva = new Reserva(amigo, revista);

            repositorioReserva.Cadastrar(reserva);
        }
    }
}