namespace Domain;

public class Pagamento
{
    public int VendaId { get; set; }
    public string FormaPagamento { get; set; } // credito, debito, dinheiro
    public decimal Valor { get; set; }

    public Pagamento() {}

    public Pagamento(int vendaId, string forma, decimal valor)
    {
        VendaId = vendaId;
        FormaPagamento = forma;
        Valor = valor;
    }
}