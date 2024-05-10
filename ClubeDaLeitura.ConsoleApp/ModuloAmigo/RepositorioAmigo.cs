using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo
{
    internal class RepositorioAmigo : RepositorioBase
    {
        private List<Amigo> amigos = new List<Amigo>();

        public void Atualizar(Amigo amigoAtualizado)
        {
            for (int i = 0; i < amigos.Count; i++)
            {
                Amigo amigoExistente = amigos[i];
                if (amigoExistente.Id == amigoAtualizado.Id)
                {
                    amigos[i] = amigoAtualizado;
                    break;
                }
            }
        }
    }
}