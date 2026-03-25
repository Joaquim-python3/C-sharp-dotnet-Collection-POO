using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain;

public class CarrinhoDeCompras
{

    public int id { get; set; }

    public int ClienteId { get; set; }
    public DateTime CriadoEm { get; set; }
    public List<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();
    public Funcionario Funcionario { get; set; }
    public string Status { get; set; } = "ativo";

    public CarrinhoDeCompras()
    {
    }

    // Venda presencial (nao precisa de Cliente)
    public CarrinhoDeCompras(int id, DateTime dataVenda, Funcionario funcionario)
    {
        this.id = id;
        CriadoEm = dataVenda;
        //Itens = new List<Produto>();
        Funcionario = funcionario;
    }

    // Venda online (nao precisa de Funcionario)
    public CarrinhoDeCompras(int id, DateTime dataVenda, Cliente cliente)
    {
        this.id = id;
        CriadoEm = dataVenda;
        //Cliente = cliente;
        //Carrinho = new List<Produto>();
    }

    public void AdicionarAoCarrinho(Produto newProduto)
    {
        //this.Carrinho.Add(newProduto);
        Console.WriteLine("Produto adicionado com sucesso!");
    }

}
