using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo
{
    internal class RepositorioAmigo : RepositorioBase
    {
        public Amigo SelecionarPorNome(string nome)
        {
            foreach (Amigo amigo in registros)
            {
                if (amigo.Nome == nome)
                    return amigo;
            }

            return null;
        }
    }
}