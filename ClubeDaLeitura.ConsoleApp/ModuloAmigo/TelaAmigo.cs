using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo
{
    internal class TelaAmigo : TelaBase
    {
        private RepositorioAmigo repositorioAmigo = RepositorioAmigo.Instancia;

        public override char ApresentarMenu()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"|        Gestão de {tipoEntidade}s        |");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            Console.WriteLine($"1 - Cadastrar {tipoEntidade}");
            Console.WriteLine($"2 - Multas de {tipoEntidade}s");
            Console.WriteLine($"3 - Editar {tipoEntidade}");
            Console.WriteLine($"4 - Excluir {tipoEntidade}s");
            Console.WriteLine($"5 - Visualizar {tipoEntidade}s");

            Console.WriteLine("S - Voltar");

            Console.WriteLine();

            Console.Write("Escolha uma das opções: ");
            char operacaoEscolhida = Convert.ToChar(Console.ReadLine());

            if (operacaoEscolhida == '2')
            {
                Console.Clear();

                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"        Multa de {tipoEntidade}s        ");
                Console.WriteLine("----------------------------------------");

                Console.WriteLine();

                Console.WriteLine($"1 - Visualizar amigos com multas");
                Console.WriteLine($"2 - Quitar multas");

                Console.WriteLine("S - Voltar");

                Console.WriteLine();

                Console.Write("Escolha uma das opções: ");
                char operacaoMultaEscolhida = Convert.ToChar(Console.ReadLine());

                if (operacaoMultaEscolhida == '1')
                {
                    VisualizarAmigosComMultas();
                    return 'S';
                }
                else if (operacaoMultaEscolhida == '2')
                {
                    QuitarMultas();
                    return 'S';
                }
            }

            return operacaoEscolhida;
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine("Visualizando Amigos...");
            }

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -25} | {2, -25} | {3, -25} | {4, -25}",
                "Id", "Nome", "NomeResponsavel", "Telefone", "Endereco"
            );

            ArrayList amigosCadastrados = repositorio.SelecionarTodos();

            foreach (Amigo amigo in amigosCadastrados)
            {
                if (amigo == null)
                    continue;

                Console.WriteLine(
                    "{0, -10} | {1, -25} | {2, -25} | {3, -25} | {4, -25}",
                    amigo.Id, amigo.Nome, amigo.NomeResponsavel, amigo.Telefone, amigo.Endereco
                );
            }

            Console.ReadLine();
            Console.WriteLine();
        }

        protected override EntidadeBase ObterRegistro()
        {
            Console.Write("Digite o nome do amigo: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o Nome do responsável do amigo: ");
            string nomeResponsavel = Console.ReadLine();

            Console.Write("Digite o telefone do amigo: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite o endereço do amigo: ");
            string endereco = Console.ReadLine();

            Amigo novoAmigo = new Amigo(nome, nomeResponsavel, telefone, endereco);

            return novoAmigo;
        }

        public void VisualizarAmigosComMultas()
        {
            bool temAmigoComMulta = false;
            Console.WriteLine("\nAmigos com multas:");

            foreach (Amigo amigo in repositorioAmigo.SelecionarTodos())
            {
                if (amigo.Multa > 0)
                {
                    temAmigoComMulta = true;
                    Console.WriteLine($"ID: {amigo.Id}, Nome: {amigo.Nome}, Multa: {amigo.Multa}");
                }
            }

            if (!temAmigoComMulta)
            {
                Console.WriteLine("\nNão existem amigos com multas");
            }

            Console.ReadLine();
            Console.WriteLine();
        }

        public void QuitarMultas()
        {
            Console.WriteLine("\nQuitar multas de amigos:");

            VisualizarAmigosComMultas();

            Console.Write("\nDigite o ID do amigo que deseja quitar a multa: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine());

            Amigo amigo = (Amigo)repositorioAmigo.SelecionarPorId(idAmigo);

            if (amigo != null && amigo.Multa > 0)
            {
                amigo.Multa = 0;
                Console.WriteLine($"\nA multa do amigo {amigo.Nome} foi quitada.");
            }
            else
            {
                Console.WriteLine("\nAmigo não encontrado ou não possui multa.");
            }

            Console.ReadLine();
            Console.WriteLine();
        }

        public void CadastrarEntidadeTeste()
        {
            Amigo amigo1 = new Amigo("Joao Silva", "Pedro Silva", "49 99999-9521", "Rua Z5");
            repositorio.Cadastrar(amigo1);

            Amigo amigo2 = new Amigo("Paulo Souza", "Luciano Souza", "49 99876-4321", "Rua G5");
            repositorio.Cadastrar(amigo2);

            Amigo amigo3 = new Amigo("Wesley Santos", "Maicon Santos", "49 12345-6789", "Rua D9");
            repositorio.Cadastrar(amigo3);
        }
    }
}