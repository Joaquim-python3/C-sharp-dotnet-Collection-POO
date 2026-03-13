// Aqui deve conter as classes obrigatórias (Loja, funcionario, cliente, produto etc)

using System.Collections;

namespace Domain;

public class Produto
{
    public int id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }

    public string Categoria { get; set; }

    public List<string> Tags { get; set; }
    public Produto(int id, string nome, decimal preco, string categoria, List<string> tags)
    {
        this.id = id;
        Nome = nome;
        Preco = preco;
        Categoria = categoria;
        Tags = tags;
    }

    public Produto()
    {
    }
}