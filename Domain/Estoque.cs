namespace Domain;

public class Estoque
{
    public int Id{get; set;}
    public int ProdutoId{get; set;}
    public int LojaId{get; set;}
    public decimal Quantidade{get; set;}

    public Estoque(){}

    public Estoque(int produtoId, int lojaId, decimal quantidade)
    {
        ProdutoId = produtoId;
        LojaId = lojaId;
        Quantidade = quantidade;

    }

    public override string ToString()
    {
        return $"ProdutoId: {ProdutoId} | LojaId: {LojaId} | Quantidade: {Quantidade}";
    }

}