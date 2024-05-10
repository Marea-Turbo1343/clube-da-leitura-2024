using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    internal class RepositorioEmprestimo : RepositorioBase
    {
        public Emprestimo SelecionarPorRevista(Revista revista)
        {
            foreach (Emprestimo emprestimo in registros)
            {
                if (emprestimo.Revista == revista && !emprestimo.Concluido)
                {
                    return emprestimo;
                }
            }

            return null;
        }
    }
}