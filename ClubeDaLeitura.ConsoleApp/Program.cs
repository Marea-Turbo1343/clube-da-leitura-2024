using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
            TelaAmigo telaAmigo = new TelaAmigo();
            telaAmigo.tipoEntidade = "Amigo";
            telaAmigo.repositorio = repositorioAmigo;
            telaAmigo.CadastrarEntidadeTeste();

            RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
            TelaCaixa telaCaixa = new TelaCaixa();
            telaCaixa.tipoEntidade = "Caixa";
            telaCaixa.repositorio = repositorioCaixa;
            telaCaixa.CadastrarEntidadeTeste();

            RepositorioRevista repositorioRevista = new RepositorioRevista();
            TelaRevista telaRevista = new TelaRevista();
            telaRevista.tipoEntidade = "Revista";
            telaRevista.repositorio = repositorioRevista;
            telaRevista.repositorioCaixa = repositorioCaixa;
            telaRevista.telaCaixa = telaCaixa;
            telaRevista.CadastrarEntidadeTeste();

            RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();
            RepositorioReserva repositorioReserva = new RepositorioReserva();
            TelaEmprestimo telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, repositorioAmigo, repositorioRevista, repositorioReserva);
            telaEmprestimo.tipoEntidade = "Emprestimo";
            telaEmprestimo.CadastrarEntidadeTeste();

            TelaReserva telaReserva = new TelaReserva(repositorioReserva, repositorioAmigo, repositorioRevista, repositorioEmprestimo);
            telaReserva.tipoEntidade = "Reserva";
            telaReserva.CadastrarEntidadeTeste();

            while (true)
            {
                char operacaoEscolhida;

                char opcaoPrincipalEscolhida = TelaPrincipal.ApresentarMenuPrincipal();

                if (opcaoPrincipalEscolhida == 'S' || opcaoPrincipalEscolhida == 's')
                    break;

                TelaBase tela = null;

                if (opcaoPrincipalEscolhida == '1')
                {
                    tela = telaAmigo;
                    operacaoEscolhida = tela.ApresentarMenu();

                    if (operacaoEscolhida == 'S' || operacaoEscolhida == 's')
                        continue;

                    if (operacaoEscolhida == '1')
                        tela.Registrar();

                    else if (operacaoEscolhida == '3')
                        tela.Editar();

                    else if (operacaoEscolhida == '4')
                        tela.Excluir();

                    else if (operacaoEscolhida == '5')
                        tela.VisualizarRegistros(true);
                }

                else if (opcaoPrincipalEscolhida == '2')
                    tela = telaCaixa;

                else if (opcaoPrincipalEscolhida == '3')
                    tela = telaRevista;

                else if (opcaoPrincipalEscolhida == '4')
                {
                    tela = telaEmprestimo;
                    operacaoEscolhida = tela.ApresentarMenu();

                    if (operacaoEscolhida == 'S' || operacaoEscolhida == 's')
                        continue;

                    if (operacaoEscolhida == '1')
                        tela.Registrar();

                    else if (operacaoEscolhida == '2')
                        tela.Editar();

                    else if (operacaoEscolhida == '3')
                        tela.Excluir();

                    else if (operacaoEscolhida == '4')
                        tela.VisualizarRegistros(true);

                    else if (operacaoEscolhida == '5')
                    {
                        telaEmprestimo.ConcluirEmprestimo();
                    }
                }

                else if (opcaoPrincipalEscolhida == '5')
                {
                    tela = telaReserva;
                    operacaoEscolhida = tela.ApresentarMenu();

                    if (operacaoEscolhida == 'S' || operacaoEscolhida == 's')
                        continue;

                    if (operacaoEscolhida == '1')
                        tela.Registrar();

                    else if (operacaoEscolhida == '2')
                        tela.Editar();

                    else if (operacaoEscolhida == '3')
                        tela.Excluir();

                    else if (operacaoEscolhida == '4')
                        tela.VisualizarRegistros(true);
                }
            }
        }
    }
}