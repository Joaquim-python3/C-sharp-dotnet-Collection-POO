namespace Business;

using Domain;
using MySql.Data.MySqlClient;

public class EstoqueRepository
{
    private Database database;

    public EstoqueRepository(Database db)
    {
        database = db;
    }

    public void AdicionarEstoque(int produtoId, int lojaId, decimal quantidade)
    {
        using var conn = database.GetConnection();
        conn.Open();

        string sql = @"INSERT INTO estoque (produto_id, loja_id, quantidade) VALUES (@produto_id, @loja_id, @quantidade)";

        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@produto_id", produtoId);
        cmd.Parameters.AddWithValue("@loja_id", lojaId);
        cmd.Parameters.AddWithValue("@quantidade", quantidade);

        cmd.ExecuteNonQuery();

    }

    public void ReporEstoque(int produtoId, int lojaId, decimal quantidade)
    {
        using var conn = database.GetConnection();
        conn.Open();

        string sql = @"UPDATE estoque 
                    SET quantidade = quantidade + @quantidade
                    WHERE produto_id = @produto_id 
                    AND loja_id = @loja_id";

        var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@quantidade", quantidade);
        cmd.Parameters.AddWithValue("@produto_id", produtoId);
        cmd.Parameters.AddWithValue("@loja_id", lojaId);

        int linhasAfetadas = cmd.ExecuteNonQuery();

        if (linhasAfetadas == 0)
        {
            Console.WriteLine("Produto não encontrado no estoque dessa loja.");
        }
        else
        {
            Console.WriteLine("Estoque atualizado com sucesso!");
        }
    }

    public Estoque ObterEstoque(int produtoId, int lojaId)
    {
        using var conn = database.GetConnection();
        conn.Open();

        string sql = "SELECT * FROM estoque WHERE produto_id=@p AND loja_id=@l";

        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@p", produtoId);
        cmd.Parameters.AddWithValue("@l", lojaId);

        var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new Estoque
            {
                Id = Convert.ToInt32(reader["id"]),
                ProdutoId = Convert.ToInt32(reader["produto_id"]),
                LojaId = Convert.ToInt32(reader["loja_id"]),
                Quantidade = Convert.ToDecimal(reader["quantidade"])
            };
        }

        return null;
    }

    public void BaixarEstoque(int produtoId, int lojaId, decimal quantidade)
    {
        using var conn = database.GetConnection();
        conn.Open();

        string sqlSelect = @"SELECT quantidade 
                            FROM estoque 
                            WHERE produto_id = @produto_id 
                            AND loja_id = @loja_id";

        var cmdSelect = new MySqlCommand(sqlSelect, conn);
        cmdSelect.Parameters.AddWithValue("@produto_id", produtoId);
        cmdSelect.Parameters.AddWithValue("@loja_id", lojaId);

        var result = cmdSelect.ExecuteScalar();

        // Se o produto não existir
        if (result == null)
        {
            Console.WriteLine("Produto não encontrado no estoque.");
            return;
        }

        decimal quantidadeAtual = Convert.ToDecimal(result);

        // Se q quantidade de produtos retirados > quantidade atual
        if (quantidade > quantidadeAtual)
        {
            Console.WriteLine($"Erro: estoque insuficiente. Disponível: {quantidadeAtual}");
            return;
        }

        // Diminui o estoque
        string sqlUpdate = @"UPDATE estoque 
                            SET quantidade = quantidade - @quantidade
                            WHERE produto_id = @produto_id 
                            AND loja_id = @loja_id";

        var cmdUpdate = new MySqlCommand(sqlUpdate, conn);

        cmdUpdate.Parameters.AddWithValue("@quantidade", quantidade);
        cmdUpdate.Parameters.AddWithValue("@produto_id", produtoId);
        cmdUpdate.Parameters.AddWithValue("@loja_id", lojaId);

        cmdUpdate.ExecuteNonQuery();

        Console.WriteLine("Baixa no estoque realizada com sucesso!");
    }

    public void ListarEstoqueGeral()
    {
        using var conn = database.GetConnection();
        conn.Open();

        // SQL com JOIN para buscar os nomes reais das tabelas relacionadas
        string sql = @"
            SELECT 
                e.id, 
                p.nome AS produto, 
                l.nome AS loja, 
                e.quantidade 
            FROM estoque e
            INNER JOIN produtos p ON e.produto_id = p.id
            INNER JOIN lojas l ON e.loja_id = l.id";

        var cmd = new MySqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();

        Console.WriteLine("\n--- LISTA DE ESTOQUE ---");

        while (reader.Read())
        {
            // Amostragem simples e direta
            Console.WriteLine($"ID: {reader["id"]} | Produto: {reader["produto"]} | Loja: {reader["loja"]} | Qtd: {reader["quantidade"]}");
        }
        
        Console.WriteLine("------------------------\n");
    }

}