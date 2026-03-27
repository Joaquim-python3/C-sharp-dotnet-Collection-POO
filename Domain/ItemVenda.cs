namespace Domain;

public class ItemVenda
{
    public int ProdutoId { get; set; }
    public decimal Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }

    public ItemVenda() {}

    public ItemVenda(int produtoId, decimal quantidade, decimal preco)
    {
        ProdutoId = produtoId;
        Quantidade = quantidade;
        PrecoUnitario = preco;
    }
}