namespace Domain;

public class ItemCarrinho
{
    public int id {get; set;}
    public int CarrinhoId {get; set;}
    public int ProdutoId {get;set;}
    public decimal Quantidade {get; set;}
    public string NomeProduto { get; set; }
    public decimal Preco { get; set; }
}