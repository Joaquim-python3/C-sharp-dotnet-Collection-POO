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
    public Venda()
    {
    }

    public Venda(int id, DateTime dataVenda, List<Produto> carrinho)
    {
        this.id = id;
        DataVenda = dataVenda;
        Carrinho = carrinho;
    }
}
