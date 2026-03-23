using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain;

public class CarrinhoDeCompras
{

    public int id { get; set; }
    public DateTime DataVenda { get; set; }
    public List<Produto> Carrinho { get; set; }
    public Cliente Cliente { get; set; }
    public Funcionario Funcionario { get; set; }

    public CarrinhoDeCompras()
    {
    }

    // Venda presencial (nao precisa de Cliente)
    public CarrinhoDeCompras(int id, DateTime dataVenda, Funcionario funcionario)
    {
        this.id = id;
        DataVenda = dataVenda;
        Carrinho = new List<Produto>();
        Funcionario = funcionario;
    }

    // Venda online (nao precisa de Funcionario)
    public CarrinhoDeCompras(int id, DateTime dataVenda, Cliente cliente)
    {
        this.id = id;
        DataVenda = dataVenda;
        Cliente = cliente;
        Carrinho = new List<Produto>();
    }

    public void AdicionarAoCarrinho(Produto newProduto)
    {
        this.Carrinho.Add(newProduto);
        Console.WriteLine("Produto adicionado com sucesso!");
    }

}
