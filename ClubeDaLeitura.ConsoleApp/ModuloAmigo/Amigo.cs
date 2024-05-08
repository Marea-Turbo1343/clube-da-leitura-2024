namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo
{
    public class Amigo
    {
        public int Endereco { get; set; }

        public int Nome { get; set; }

        public int NomeResponsavel { get; set; }

        public int Telefone { get; set; }

        public ClubeDaLeitura.ConsoleApp.ModuloMulta.Multa[] HistoricoDeMulta { get; set; }

    }
}