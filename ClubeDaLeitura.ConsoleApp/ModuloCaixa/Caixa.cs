﻿using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa
{
    internal class Caixa : EntidadeBase
    {
        public string Etiqueta { get; set; }
        public string Cor { get; set; }
        public int DiasEmprestimo { get; private set; }
        public int ValorMulta { get; private set; }
        public int Tipo { get; set; }

        public Caixa(string etiqueta, string cor, int tipo)
        {
            Etiqueta = etiqueta;
            Cor = cor;
            Tipo = tipo;

            if (Tipo == 1)
            {
                DiasEmprestimo = 7;
                ValorMulta = 2;
            }
            else if (Tipo == 2)
            {
                DiasEmprestimo = 3;
                ValorMulta = 5;
            }
        }

        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();

            if (string.IsNullOrEmpty(Etiqueta.Trim()))
                erros.Add("O campo \"etiqueta\" é obrigatório");

            if (string.IsNullOrEmpty(Cor.Trim()))
                erros.Add("O campo \"cor\" é obrigatório");

            if (DiasEmprestimo <= 0)
                erros.Add("O campo \"diasEmprestimo\" deve ser maior que zero");

            if (ValorMulta < 0)
                erros.Add("O campo \"valorMulta\" não pode ser negativo");

            if (Tipo != 1 && Tipo != 2)
                erros.Add("O campo \"tipo\" deve ser 1 (PADRÃO) ou 2 (NOVIDADE)");

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Caixa novasInformacoes = (Caixa)novoRegistro;

            this.Etiqueta = novasInformacoes.Etiqueta;
            this.Cor = novasInformacoes.Cor;
            this.DiasEmprestimo = novasInformacoes.DiasEmprestimo;
            this.ValorMulta = novasInformacoes.ValorMulta;
            this.Tipo = novasInformacoes.Tipo;
        }
    }
}