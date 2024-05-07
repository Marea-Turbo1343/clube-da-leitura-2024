using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    public class Emprestimo
    {
        public int Concluido { get; set; }

        public int Data { get; set; }

        public int DataDevolucao { get; set; }

        public Amigo Amigo { get; set; }

        public Revista Revista { get; set; }

        public Amigo Amigo1
        {
            get => default;
            set
            {
            }
        }
    }
}