using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioAutomacaoAPIBase2.Models
{
    public class Produtos
    {

        public Produtos() { }

        public string nome { get; set; }
        public int preco { get; set; }
        public string descricao { get; set; }
        public int quantidade { get; set; }

        public Produtos(string nome, int preco, string descricao, int quantidade)
        {
            this.nome = nome;
            this.preco = preco;
            this.descricao = descricao;
            this.quantidade = quantidade;
        }
    }
}
