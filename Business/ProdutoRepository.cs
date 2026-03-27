// Aqui deve conter as classes relacionadas a regras de negócios (Métodos CRUD e etc)
// Produto não possui quantidade, serve SOMENTE para catálogo. Estoque terá quantidade
namespace Business;

using MySql.Data.MySqlClient;
using Domain;

public class ProdutoRepository
{
    private Database database;

    public ProdutoRepository(Database db)
    {
        database = db;
    }

    // CREATE
    public int CriarProdutos(Produto produto)
    {
        using var conn = database.GetConnection();
        conn.Open();

        // Inserir Produto (Catálogo)
        string sql = "INSERT INTO produtos (nome, preco, tipo_venda, categoria_id) VALUES (@nome, @preco, @tipo_venda, @categoria_id)";

        var cmd = new MySqlCommand(sql, conn); // Comandos relacionados a produto
        cmd.Parameters.AddWithValue("@nome", produto.Nome);
        cmd.Parameters.AddWithValue("@preco", produto.Preco);
        cmd.Parameters.AddWithValue("@tipo_venda", produto.TipoVenda);
        cmd.Parameters.AddWithValue("@categoria_id", produto.Categoria_id);

        cmd.ExecuteNonQuery();

        return (int)cmd.LastInsertedId;

    }

    // READ
    public void ListarProdutos()
    {
        using var conn = database.GetConnection();
        conn.Open();

        string sql = "SELECT * FROM produtos";

        var cmd = new MySqlCommand(sql, conn);
        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["id"]} - {reader["nome"]} - {reader["preco"]} - {reader["tipo_venda"]}");
        }
    }

    // UPDATE
    public void AtualizarProdutos(int id, Produto produto)
    {
        using var conn = database.GetConnection();
        conn.Open();

        string sql = "UPDATE produtos SET nome=@nome, preco=@preco WHERE id=@id";

        var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@nome", produto.Nome);
        cmd.Parameters.AddWithValue("@preco", produto.Preco);
        cmd.Parameters.AddWithValue("@tipo_venda", produto.TipoVenda);
        cmd.Parameters.AddWithValue("@categoria_id", produto.Categoria_id);

        cmd.ExecuteNonQuery();
    }

    // DELETE
    public void DeletarProdutos(int id)
    {
        using var conn = database.GetConnection();
        conn.Open();

        string sql = "DELETE FROM produtos WHERE id=@id";

        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

    // READ BY ID
    public Produto? ProcurarProdutoPeloId(string id)
    {
        Produto p = new Produto();
        using var conn = database.GetConnection();
        conn.Open();

        string sql = "SELECT * FROM produtos WHERE id=@id";

        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);
        var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Console.WriteLine($"{reader["id"]} - {reader["nome"]} - {reader["preco"]} - {reader["tipo_venda"]}");
            return new Produto
            (
            /*ID*/          Convert.ToInt32(reader["id"]),
            /*NOME*/        reader["nome"].ToString(),
            /*PRECO*/       Convert.ToDecimal(reader["preco"]),
            /*TipoVenda*/   reader["tipo_venda"].ToString(),
            /*CATEGORIA*/   Convert.ToInt32(reader["categoria_id"]),
            /*TAGS*/        new List<string>() // depois você pode puxar isso de outra tabela
            );
        }

        return null;

    }

}