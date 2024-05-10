using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva
{
    internal class Reserva : EntidadeBase
    {
        public Amigo Amigo { get; set; }
        public Revista Revista { get; set; }
        public DateTime DataReserva { get; set; }
        public bool Expirada { get; set; }

        public Reserva(Amigo amigo, Revista revista)
        {
            Amigo = amigo;
            Revista = revista;
            DataReserva = DateTime.Now;
            Expirada = false;
        }

        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();

            if (Amigo == null)
                erros.Add("O campo \"Amigo\" é obrigatório");

            if (Revista == null)
                erros.Add("O campo \"Revista\" é obrigatório");

            return erros;
        }

        public void VerificarExpiracao()
        {
            DateTime dataHoje = DateTime.Now;

            if (dataHoje > DataReserva.AddDays(2))
            {
                Expirada = true;
            }
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Reserva novasInformacoes = (Reserva)novoRegistro;

            this.Amigo = novasInformacoes.Amigo;
            this.Revista = novasInformacoes.Revista;
            this.DataReserva = novasInformacoes.DataReserva;
            this.Expirada = novasInformacoes.Expirada;
        }
    }
}