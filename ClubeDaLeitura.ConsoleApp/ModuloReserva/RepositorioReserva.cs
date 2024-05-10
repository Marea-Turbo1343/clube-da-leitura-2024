using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva
{
    internal class RepositorioReserva : RepositorioBase
    {
        public Reserva SelecionarPorRevista(Revista revista)
        {
            foreach (Reserva reserva in registros)
            {
                if (reserva.Revista == revista && !reserva.Expirada)
                {
                    return reserva;
                }
            }

            return null;
        }
    }
}