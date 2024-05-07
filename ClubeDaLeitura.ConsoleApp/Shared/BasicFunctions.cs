namespace ClubeDaLeitura.ConsoleApp.Functions
{
    internal class BasicFunctions
    {
        #region Caixa de Objetos
        TelaMensagens mensagem;
        #endregion

        public int LerInt(string texto)
        {
            while (true)
            {
                Console.Write(texto);
                var digitouNumero = int.TryParse(Console.ReadLine(), out var numero);
                if (digitouNumero)
                {
                    return numero;
                }
                mensagem.Erro();
            }
        }
        public bool ChecagemLong(string texto)
        {
            while (true)
            {
                var digitouNumero = long.TryParse(texto, out var numero);
                if (digitouNumero)
                {
                    return true;
                }
                mensagem.Erro();
            }
        }

        #region AindaNãoUsei
        public decimal LerDecimal(string texto)
        {
            while (true)
            {
                Console.Write(texto);
                var digitouNumero = decimal.TryParse(Console.ReadLine(), out var numero);

                if (digitouNumero)
                {
                    return numero;
                }
                mensagem.Erro();
            }
        }
        #endregion
    }
}
