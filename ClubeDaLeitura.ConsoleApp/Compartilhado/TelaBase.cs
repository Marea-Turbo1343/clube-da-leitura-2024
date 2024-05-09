using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.Compartilhado
{
    internal abstract class TelaBase
    {
        public string tipoEntidade = "";
        public RepositorioBase repositorio = null;

        public virtual char ApresentarMenu()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"        Gestão de {tipoEntidade}s        ");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            Console.WriteLine($"1 - Cadastrar {tipoEntidade}");
            Console.WriteLine($"2 - Editar {tipoEntidade}");
            Console.WriteLine($"3 - Excluir {tipoEntidade}");
            Console.WriteLine($"4 - Visualizar {tipoEntidade}s");

            Console.WriteLine("S - Voltar");

            Console.WriteLine();

            Console.Write("Escolha uma das opções: ");
            char operacaoEscolhida = Convert.ToChar(Console.ReadLine());

            return operacaoEscolhida;
        }

        protected void InserirRegistro(EntidadeBase entidade)
        {
            ArrayList erros = entidade.Validar();

            if (erros.Count > 0)
            {
                ApresentarErros(erros);
                return;
            }

            repositorio.Cadastrar(entidade);

            ExibirMensagem($"O {tipoEntidade} foi cadastrado com sucesso!", ConsoleColor.Green);
        }

        public virtual void Registrar()
        {
            ApresentarCabecalho();

            Console.WriteLine($"Cadastrando {tipoEntidade}...");

            Console.WriteLine();

            EntidadeBase entidade = ObterRegistro();

            InserirRegistro(entidade);
        }

        public void Editar()
        {
            ApresentarCabecalho();

            Console.WriteLine($"Editando {tipoEntidade}...");

            Console.WriteLine();

            VisualizarRegistros(false);

            Console.Write($"Digite o ID do {tipoEntidade} que deseja editar: ");
            int idEntidadeEscolhida = Convert.ToInt32(Console.ReadLine());

            if (!repositorio.Existe(idEntidadeEscolhida))
            {
                ExibirMensagem($"O {tipoEntidade} mencionado não existe!", ConsoleColor.DarkYellow);
                return;
            }

            Console.WriteLine();

            EntidadeBase entidade = ObterRegistro();

            ArrayList erros = entidade.Validar();

            if (erros.Count > 0)
            {
                ApresentarErros(erros);
                return;
            }

            bool conseguiuEditar = repositorio.Editar(idEntidadeEscolhida, entidade);

            if (!conseguiuEditar)
            {
                ExibirMensagem($"Houve um erro durante a edição de {tipoEntidade}", ConsoleColor.Red);
                return;
            }

            ExibirMensagem($"O {tipoEntidade} foi editado com sucesso!", ConsoleColor.Green);
        }

        public void Excluir()
        {
            ApresentarCabecalho();

            Console.WriteLine($"Excluindo {tipoEntidade}...");

            Console.WriteLine();

            VisualizarRegistros(false);

            Console.Write($"Digite o ID do {tipoEntidade} que deseja excluir: ");
            int idRegistroEscolhido = Convert.ToInt32(Console.ReadLine());

            if (!repositorio.Existe(idRegistroEscolhido))
            {
                ExibirMensagem($"O {tipoEntidade} mencionado não existe!", ConsoleColor.DarkYellow);
                return;
            }

            bool conseguiuExcluir = repositorio.Excluir(idRegistroEscolhido);

            if (!conseguiuExcluir)
            {
                ExibirMensagem($"Houve um erro durante a exclusão do {tipoEntidade}", ConsoleColor.Red);
                return;
            }

            ExibirMensagem($"O {tipoEntidade} foi excluído com sucesso!", ConsoleColor.Green);
        }

        public abstract void VisualizarRegistros(bool exibirTitulo);

        protected void ApresentarErros(ArrayList erros)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < erros.Count; i++)
                Console.WriteLine(erros[i]);

            Console.ResetColor();
            Console.ReadLine();
        }

        protected void ApresentarCabecalho()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("|           Clube da Leitura           |");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();
        }

        public void ExibirMensagem(string mensagem, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;

            Console.WriteLine();

            Console.WriteLine(mensagem);

            Console.ResetColor();

            Console.ReadLine();
        }

        protected abstract EntidadeBase ObterRegistro();
    }
}