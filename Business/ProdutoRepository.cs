// Aqui deve conter as classes relacionadas a regras de negócios (Métodos CRUD e etc)
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
    public void CriarProdutos(Produto produto)
    {
        using var conn = database.GetConnection();
        conn.Open();

        string sql = "INSERT INTO produtos (nome, preco) VALUES (@nome, @preco)";

        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@nome", produto.Nome);
        cmd.Parameters.AddWithValue("@preco", produto.Preco);

        cmd.ExecuteNonQuery();
        Console.WriteLine("Produto criado com sucesso!");
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
            Console.WriteLine($"{reader["id"]} - {reader["nome"]} - {reader["preco"]}");
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
}