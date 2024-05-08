namespace ClubeDaLeitura.ConsoleApp.Compartilhado
{
    public class TelaMensagens
    {
        //public RepositorioMedicamento RMedicamento = new RepositorioMedicamento();

        #region Apenas Mensagens
        public void Cabecalho()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Clear();
            Console.WriteLine("########################################################################################################################");
            Console.WriteLine("###                                                                                                                  ###");
            Console.WriteLine("###                                           Academia do Programador 2024                                           ###");
            Console.WriteLine("###                                                                                                                  ###");
            Console.WriteLine("###                                                   Marea Turbo                                                    ###");
            Console.WriteLine("###                                                                                                                  ###");
            Console.WriteLine("###                                                Clube do Leitura                                                  ###");
            Console.WriteLine("###                                                                                                                  ###");
            Console.WriteLine("########################################################################################################################\n");
            Console.ResetColor();
        }
        public void Erro()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Clear();
            Console.WriteLine("\n\n\n########################################################################################################################");
            Console.WriteLine("###                                                                                                                  ###");
            Console.WriteLine("###                                                      ATENÇÃO                                                     ###");
            Console.WriteLine("###                                                                                                                  ###");
            Console.WriteLine("###                               Comando inválido. Por favor digite um comando válido.                              ###");
            Console.WriteLine("###                                                                                                                  ###");
            Console.WriteLine("###                                      Precione qualquer tecla para continuar.                                     ###");
            Console.WriteLine("###                                                                                                                  ###");
            Console.WriteLine("########################################################################################################################");
            Console.ReadKey();
            Cabecalho();
        }
        public void ErroSemDadosCadastradosFuncionario()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Clear();
            Console.WriteLine("\n\n\n########################################################################################################################");
            Console.WriteLine("###                                                                                                                  ###");
            Console.WriteLine("###                                                      ATENÇÃO                                                     ###");
            Console.WriteLine("###                                                                                                                  ###");
            Console.WriteLine("###                             Nenhum cadastrado encontrado, direcionando para o cadastro                           ###");
            Console.WriteLine("###                                                                                                                  ###");
            Console.WriteLine("###                                      Precione qualquer tecla para continuar.                                     ###");
            Console.WriteLine("###                                                                                                                  ###");
            Console.WriteLine("########################################################################################################################");
            Console.ReadKey();
        }
        public void DataInvalida()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Clear();
            Console.WriteLine("\n\n\n########################################################################################################################");
            Console.WriteLine("###                                                                                                                  ###");
            Console.WriteLine("###                                                      ATENÇÃO                                                     ###");
            Console.WriteLine("###                                                                                                                  ###");
            Console.WriteLine("###                      Data Inválida. Por favor digite um dia igual ou inferior ao dia atual.                      ###");
            Console.WriteLine("###                                                                                                                  ###");
            Console.WriteLine("###                                      Precione qualquer tecla para continuar.                                     ###");
            Console.WriteLine("###                                                                                                                  ###");
            Console.WriteLine("########################################################################################################################");
            Console.ReadKey();
            Cabecalho();
        }
        public void CadastroComSucesso()
        {
            Cabecalho();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Cadastro efetuado com sucesso!\n\nPrecione qualquer tecla para continuar.");
            Console.ResetColor();
        }
        public void AlteracaoEfetuadaComSucesso()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\tAlteração efetuada com sucesso. Precione qualquer tecla para continuar");
            Console.ResetColor();
            Console.ReadKey();
        }
        #endregion

        #region Mensagens com Opções

        #endregion

        #region Mensagens com Funções


        #endregion

    }
}
