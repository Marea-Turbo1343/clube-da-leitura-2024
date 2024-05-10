using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo
{
    internal class RepositorioAmigo : RepositorioBase
    {
        private static RepositorioAmigo instancia;

        private RepositorioAmigo() { }

        public static RepositorioAmigo Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new RepositorioAmigo();
                }
                return instancia;
            }
        }
        public void Atualizar(Amigo amigoAtualizado)
        {
            for (int i = 0; i < registros.Count; i++)
            {
                Amigo amigoExistente = (Amigo)registros[i];
                if (amigoExistente.Id == amigoAtualizado.Id)
                {
                    registros[i] = amigoAtualizado;
                    break;
                }
            }
        }
    }
}