// Aqui deve conter as classes obrigatórias (Loja, funcionario, cliente, produto etc)

using System.Collections;

namespace Domain;

public class Produto
{
    public int id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public string TipoVenda { get; set; }

    public int Categoria_id { get; set; }
    public List<string> Tags { get; set; }
    public Produto()
    {
    }
    public Produto(int id, string nome, decimal preco, string tipo_venda, int categoria_id, List<string> tags)
    {
        this.id = id;
        Nome = nome;
        Preco = preco;
        TipoVenda = tipo_venda;
        Categoria_id = categoria_id;
        Tags = tags;
    }

    public override string ToString()
{
    return $"{id} - {Nome} - R$ {Preco:F2} - {Categoria_id.ToString() ?? "categoria nao informada"} - {TipoVenda}";
}
}