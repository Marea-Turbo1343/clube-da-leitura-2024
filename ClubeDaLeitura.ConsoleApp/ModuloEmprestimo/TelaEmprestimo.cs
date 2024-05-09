using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using System.Collections;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

namespace ControleMedicamentos.ConsoleApp.ModuloRequisicao.Saida
{
    internal class TelaEmprestimo : TelaBase
    {
        public TelaAmigo telaAmigo = null;
        public TelaRevista telaRevista = null;

        public RepositorioAmigo repositorioAmigo = null;
        public RepositorioRevista repositorioRevista = null;

        public override void Registrar()
        {
            ApresentarCabecalho();

            Console.WriteLine($"Cadastrando {tipoEntidade}...");

            Console.WriteLine();

            Emprestimo entidade = (Emprestimo)ObterRegistro();

            ArrayList erros = entidade.Validar();

            if (erros.Count > 0)
            {
                ApresentarErros(erros);
                return;
            }

            bool conseguiuRetirar = entidade.Concluir();

            if (!conseguiuRetirar)
            {
                ExibirMensagem("Existe multa em aberto", ConsoleColor.DarkYellow);
                return;
            }

            repositorio.Cadastrar(entidade);

            ExibirMensagem($"O {tipoEntidade} foi cadastrado com sucesso!", ConsoleColor.Green);
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine("Visualizando Requisições de Saída...");
            }

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -15} | {3, -20} | {4, -5}",
                "Id", "Medicamento", "Paciente", "Data de Requisição", "Quantidade"
            );

            ArrayList requisicoesCadastradas = repositorio.SelecionarTodos();

            foreach (RequisicaoSaida requisicao in requisicoesCadastradas)
            {
                if (requisicao == null)
                    continue;

                Console.WriteLine(
                    "{0, -10} | {1, -15} | {2, -15} | {3, -20} | {4, -5}",
                    requisicao.Id,
                    requisicao.Medicamento.Nome,
                    requisicao.Paciente.Nome,
                    requisicao.DataRequisicao.ToShortDateString(),
                    requisicao.QuantidadeRetirada
                );
            }

            Console.ReadLine();
            Console.WriteLine();
        }

        protected override EntidadeBase ObterRegistro()
        {
            telaMedicamento.VisualizarRegistros(false);

            Console.Write("Digite o ID do medicamento requisitado: ");
            int idMedicamento = Convert.ToInt32(Console.ReadLine());

            Medicamento medicamentoSelecionado = (Medicamento)repositorioMedicamento.SelecionarPorId(idMedicamento);

            telaPaciente.VisualizarRegistros(false);

            Console.Write("Digite o ID do paciente requisitante: ");
            int idPaciente = Convert.ToInt32(Console.ReadLine());

            Paciente pacienteSelecionado = (Paciente)repositorioPaciente.SelecionarPorId(idPaciente);

            Console.Write("Digite a quantidade do medicamente que deseja retirar: ");
            int quantidade = Convert.ToInt32(Console.ReadLine());

            RequisicaoSaida novaRequisicao = new RequisicaoSaida(medicamentoSelecionado, pacienteSelecionado, quantidade);

            return novaRequisicao;
        }
    }
}