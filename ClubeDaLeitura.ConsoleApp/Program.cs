namespace ClubeDaLeitura.ConsoleApp
{
    internal class Program
    {
        public static void Main()
        {
            //LerDecimalTelaEqueleto engine = new TelaEqueleto();

            int posicaoativafuncionario = 0;

            engine.ChecagemPreLogin();

            posicaoativafuncionario = engine.login.Login();

            while (true)
            {
                int opcaomenu = engine.MenuPrincipal(posicaoativafuncionario);
                switch (opcaomenu)
                {
                    case 1:
                        engine.NovoCadastro();
                        break;

                    case 2:
                        engine.MenuProntuario();
                        break;

                    case 3:
                        engine.medicamento.SolicitarRemedio();
                        break;

                    case 4:
                        engine.medicamento.VerficicarEstoque();
                        break;

                    case 5:
                        Main();
                        break;
                }
            }
        }
    }
}
}
