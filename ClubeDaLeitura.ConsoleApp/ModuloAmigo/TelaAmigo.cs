using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo
{
    internal class TelaAmigo : TelaBase
    {
        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine("Visualizando Amigos...");
            }

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -15} | {3, -15} | {4, -15}",
                "Id", "Nome", "NomeResponsavel", "Telefone", "Endereco"
            );

            ArrayList amigosCadastrados = repositorio.SelecionarTodos();

            foreach (Amigo amigo in amigosCadastrados)
            {
                if (amigo == null)
                    continue;

                Console.WriteLine(
                    "{0, -10} | {1, -15} | {2, -15} | {3, -15} | {4, -15}",
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

        public void CadastrarEntidadeTeste()
        {
            Amigo amigo = new Amigo("Bobby Tables", "Pedro", "49 9999-9521", "Rua Z5");

            repositorio.Cadastrar(amigo);
        }
    }
}