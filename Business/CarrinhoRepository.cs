namespace Business;

using MySql.Data.MySqlClient;
using Domain;

public class CarrinhoRepository
{
    private Database database;

    public CarrinhoRepository(Database db)
    {
        database = db;
    }

    public CarrinhoDeCompras ObterOuCriarCarrinho(int clienteId)
    {
        using var conn = database.GetConnection();
        conn.Open();
        
        string sql = "SELECT * FROM carrinhos WHERE cliente_id=@cliente_id AND status='ativo'";

        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@cliente_id", clienteId);

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new CarrinhoDeCompras
            {
                id = Convert.ToInt32(reader["id"]),
                ClienteId = clienteId,
                CriadoEm = Convert.ToDateTime(reader["criado_em"]),
                Status = reader["status"].ToString()
            };
        }

        reader.Close();

        // Caso não exista um carrinho ativo, cria um novo
        string insertSql = "INSERT INTO carrinhos (cliente_id, status) VALUES (@cliente_id, 'ativo')";
        var insertCmd = new MySqlCommand(insertSql, conn);
        insertCmd.Parameters.AddWithValue("@cliente_id", clienteId);

        insertCmd.ExecuteNonQuery();

        int carrinhoId = (int)insertCmd.LastInsertedId;

        return new CarrinhoDeCompras
        {
            id = carrinhoId,
            ClienteId = clienteId,
            Status = "ativo"
        };

    }

    public void AdicionarProduto(int carrinhoId, int produtoId, decimal quantidade)
    {
        using var conn = database.GetConnection();
        conn.Open();

        // verifica se já existe
        string checkSql = "SELECT COUNT(*) FROM itens_carrinho WHERE carrinho_id=@carrinho_id AND produto_id=@produto_id";

        var checkCmd = new MySqlCommand(checkSql, conn);
        checkCmd.Parameters.AddWithValue("@carrinho_id", carrinhoId);
        checkCmd.Parameters.AddWithValue("@produto_id", produtoId);

        int existe = Convert.ToInt32(checkCmd.ExecuteScalar());

        if(existe > 0) // se já existir, adiciona
        {
            string updateSql = @"UPDATE itens_carrinho 
                                 SET quantidade = quantidade + @quantidade
                                 WHERE carrinho_id=@carrinho_id AND produto_id=@produto_id";

            var updateCmd = new MySqlCommand(updateSql, conn);
            updateCmd.Parameters.AddWithValue("@quantidade", quantidade);
            updateCmd.Parameters.AddWithValue("@carrinho_id", carrinhoId);
            updateCmd.Parameters.AddWithValue("@produto_id", produtoId);

            updateCmd.ExecuteNonQuery();
        }
        else // se não existir, insere
        {
            string insertSql = @"INSERT INTO itens_carrinho (carrinho_id, produto_id, quantidade)
                                VALUES (@carrinho_id, @produto_id, @quantidade)";
            
            var insertCmd = new MySqlCommand(insertSql, conn);
            insertCmd.Parameters.AddWithValue("@carrinho_id", carrinhoId);
            insertCmd.Parameters.AddWithValue("@produto_id", produtoId);
            insertCmd.Parameters.AddWithValue("@quantidade", quantidade);

            insertCmd.ExecuteNonQuery();
        }
            
        
    }

    public List<ItemCarrinho> ListarItens(int carrinhoId)
    {
        using var conn = database.GetConnection();
        conn.Open();

    string sql = @"
        SELECT 
            ic.id,
            ic.carrinho_id,
            ic.produto_id,
            ic.quantidade,
            p.nome,
            p.preco
        FROM itens_carrinho ic
        JOIN produtos p ON p.id = ic.produto_id
        WHERE ic.carrinho_id = @carrinho_id";

        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@carrinho_id", carrinhoId);

        using var reader = cmd.ExecuteReader();

        List<ItemCarrinho> itens = new List<ItemCarrinho>();

        while (reader.Read())
        {
            itens.Add(new ItemCarrinho
            {
                id = Convert.ToInt32(reader["id"]),
                CarrinhoId = carrinhoId,
                ProdutoId = Convert.ToInt32(reader["produto_id"]),
                Quantidade = Convert.ToDecimal(reader["quantidade"]),
                NomeProduto = reader["nome"].ToString(),
                Preco = Convert.ToDecimal(reader["preco"])                
            });
        }

        return itens;
    }

    public void RemoverItem(int carrinhoId, int produtoId)
    {
        using var conn = database.GetConnection();
        conn.Open();

        string sql = "DELETE FROM itens_carrinho WHERE carrinho_id=@carrinho_id AND produto_id=@produto_id";

        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@carrinho_id", carrinhoId);
        cmd.Parameters.AddWithValue("@produto_id", produtoId);

        cmd.ExecuteNonQuery();
    }

}