using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista
{
    internal class RepositorioRevista : RepositorioBase
    {
        public Revista SelecionarPorTitulo(string titulo)
        {
            foreach (Revista revista in registros)
            {
                if (revista.Titulo == titulo)
                    return revista;
            }

            return null;
        }
    }
}