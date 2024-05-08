namespace ClubeDaLeitura.ConsoleApp.Compartilhado
{
    internal static class TelaPrincipal
    {
        public static char ApresentarMenuPrincipal()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("|           Clube da Leitura           |");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            Console.WriteLine("1 - Gestão de Amigo");
            Console.WriteLine("2 - Gestão de Caixas de Revistas");
            Console.WriteLine("3 - Gestão de Revistas");
            Console.WriteLine("4 - Gestão de Empréstimo de Revistas");
            Console.WriteLine("5 - Gestão de Reserva de Revistas");

            Console.WriteLine("S - Sair");

            Console.WriteLine();

            Console.Write("Escolha uma das opções: ");

            char opcaoEscolhida = Console.ReadLine()[0];

            return opcaoEscolhida;
        }
    }
}