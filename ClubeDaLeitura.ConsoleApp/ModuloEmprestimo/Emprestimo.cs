﻿using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using System.Collections;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    internal class Emprestimo : EntidadeBase
    {
        private RepositorioAmigo repositorioAmigo;

        public Amigo Amigo { get; set; }
        public Revista Revista { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public bool Concluido { get; set; }
        public int Multa { get; private set; }

        public Emprestimo(Amigo amigo, Revista revista, RepositorioAmigo repositorioAmigo)
        {
            this.repositorioAmigo = repositorioAmigo;
            Amigo = amigo;
            Revista = revista;
            DataEmprestimo = DateTime.Now;
            DataDevolucao = DataEmprestimo.AddDays(Revista.Caixa.DiasEmprestimo);
            Concluido = false;
        }

        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();

            if (Amigo == null)
                erros.Add("O campo \"Amigo\" é obrigatório");

            if (Revista == null)
                erros.Add("O campo \"Revista\" é obrigatório");

            return erros;
        }

        public void Concluir(RepositorioReserva repositorioReserva, RepositorioEmprestimo repositorioEmprestimo)
        {
            DateTime dataHoje = DateTime.Now;

            if (dataHoje > DataDevolucao)
            {
                TimeSpan diferenca = dataHoje - DataDevolucao;
                int diasDiferenca = diferenca.Days;
                CalcularMulta(diasDiferenca);
            }

            Concluido = true;

            Reserva reserva = repositorioReserva.SelecionarPorRevista(Revista);
            if (reserva != null && !reserva.Expirada)
            {
                Emprestimo novoEmprestimo = new Emprestimo(reserva.Amigo, reserva.Revista, repositorioAmigo);
                repositorioEmprestimo.Cadastrar(novoEmprestimo);

                reserva.Expirada = true;
                repositorioReserva.Editar(reserva.Id, reserva);

                Console.WriteLine($"A revista {Revista.Titulo} foi automaticamente emprestada para {reserva.Amigo.Nome}.");
            }
        }

        private void CalcularMulta(int diasAtraso)
        {
            int valorMultaPorDia = (int)Revista.Caixa.ValorMulta;
            Multa = valorMultaPorDia * diasAtraso;
            Amigo.Multa += Multa;

            repositorioAmigo.Atualizar(Amigo);
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Emprestimo novasInformacoes = (Emprestimo)novoRegistro;

            this.Amigo = novasInformacoes.Amigo;
            this.Revista = novasInformacoes.Revista;
            this.DataEmprestimo = novasInformacoes.DataEmprestimo;
            this.DataDevolucao = novasInformacoes.DataDevolucao;
            this.Concluido = novasInformacoes.Concluido;
        }
    }
}