using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain;

public class Venda
{

    public int id { get; set; }
    public DateTime DataVenda { get; set; }
    public List<Produto> Carrinho { get; set; }
    public Cliente Cliente { get; set; }
    public Funcionario Funcionario { get; set; }

    public Venda()
    {
    }

// Venda presencial
    public Venda(int id, DateTime dataVenda, List<Produto> carrinho, Funcionario funcionario)
    {
        this.id = id;
        DataVenda = dataVenda;
        Carrinho = carrinho;
        Funcionario = funcionario;
    }

// Venda online
    public Venda(int id, DateTime dataVenda, Cliente cliente, List<Produto> carrinho, Funcionario funcionario)
    {
        this.id = id;
        DataVenda = dataVenda;
        Cliente = cliente;
        Carrinho = carrinho;
        Funcionario = funcionario;

    }

}
