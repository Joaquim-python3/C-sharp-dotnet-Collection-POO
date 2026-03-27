namespace Domain;

public class Venda
{
    public int Id { get; set; }
    public DateTime DataVenda { get; set; }
    public int? ClienteId { get; set; } // pode ser null (loja física)
    public int? FuncionarioId { get; set; } // pode ser null (Ecommerce)
    public int LojaId { get; set; }
    public string TipoVenda { get; set; } // fisica ou online

    public List<ItemVenda> Itens { get; set; } = new List<ItemVenda>();

    public Venda() {}

    public Venda(int clienteId, int funcionarioId, int lojaId, string tipoVenda)
    {
        ClienteId = clienteId;
        FuncionarioId = funcionarioId;
        LojaId = lojaId;
        TipoVenda = tipoVenda;
        DataVenda = DateTime.Now;
    }
}
