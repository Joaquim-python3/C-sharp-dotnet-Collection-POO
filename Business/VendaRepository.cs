namespace Business;

using MySql.Data.MySqlClient;
using Domain;

public class VendaRepository{

    private Database database;

    public VendaRepository(Database db)
    {
        database = db;
    }


    public void FinalizarVenda(
        int? clienteId,
        int? funcionarioId,
        int lojaId,
        string tipoVenda,
        List<(int produtoId, decimal quantidade)> itens,
        string formaPagamento
    )
    {
        using var conn = database.GetConnection();
        conn.Open();

        // UTILIZANDO TRANSACTION PARA SEGURANÇA
        using var transaction = conn.BeginTransaction();

        try
        {

            // VERIFICAÇÕES SE É UMA VENDA FÍSICA OU ONLINE
            if (tipoVenda == "fisica" && funcionarioId == null)
                throw new Exception("Venda física precisa de funcionário!");

            if (tipoVenda == "online" && funcionarioId != null)
                throw new Exception("Venda online não deve ter funcionário!");

            string sqlVenda = @"INSERT INTO vendas (data_venda, cliente_id, funcionario_id, loja_id, tipo_venda)
                                VALUES (NOW(), @cliente_id, @funcionario_id, @loja_id, @tipo_venda)";

            var cmdVenda = new MySqlCommand(sqlVenda, conn, transaction);

            cmdVenda.Parameters.AddWithValue("@cliente_id", clienteId.HasValue ? clienteId : DBNull.Value);
            cmdVenda.Parameters.AddWithValue("@funcionario_id", funcionarioId.HasValue ? funcionarioId : DBNull.Value);
            cmdVenda.Parameters.AddWithValue("@loja_id", lojaId);
            cmdVenda.Parameters.AddWithValue("@tipo_venda", tipoVenda);

            cmdVenda.ExecuteNonQuery();

            int vendaId = (int)cmdVenda.LastInsertedId;

            decimal total = 0;

            foreach (var item in itens)
            {
                // pegar preço atual
                string sqlPreco = "SELECT preco FROM produtos WHERE id=@id";
                var cmdPreco = new MySqlCommand(sqlPreco, conn, transaction);
                cmdPreco.Parameters.AddWithValue("@id", item.produtoId);

                // Pega o valor da primeira coluna e linha
                decimal preco = Convert.ToDecimal(cmdPreco.ExecuteScalar());

                total += preco * item.quantidade;

                // Verificar se existe a quantidade no estoque
                string sqlEstoqueCheck = @"SELECT quantidade 
                          FROM estoque 
                          WHERE produto_id = @produto_id AND loja_id = @loja_id";

                var cmdCheck = new MySqlCommand(sqlEstoqueCheck, conn, transaction);
                cmdCheck.Parameters.AddWithValue("@produto_id", item.produtoId);
                cmdCheck.Parameters.AddWithValue("@loja_id", lojaId);

                var estoqueAtualObj = cmdCheck.ExecuteScalar();

                if (estoqueAtualObj == null) // Se não tiver estoque do produto
                    throw new Exception($"Produto {item.produtoId} não possui estoque cadastrado.");

                decimal estoqueAtual = Convert.ToDecimal(estoqueAtualObj);

                if (estoqueAtual < item.quantidade) // Se tiver quantidade suficiente no estoque
                {
                    throw new Exception(
                        $"Estoque insuficiente para o produto {item.produtoId}. " +
                        $"Disponível: {estoqueAtual}, solicitado: {item.quantidade}"
                    );
                }

                // inserir item
                string sqlItem = @"INSERT INTO itens_venda (venda_id, produto_id, quantidade, preco_unitario)
                                VALUES (@venda_id, @produto_id, @quantidade, @preco)";

                var cmdItem = new MySqlCommand(sqlItem, conn, transaction);

                cmdItem.Parameters.AddWithValue("@venda_id", vendaId);
                cmdItem.Parameters.AddWithValue("@produto_id", item.produtoId);
                cmdItem.Parameters.AddWithValue("@quantidade", item.quantidade);
                cmdItem.Parameters.AddWithValue("@preco", preco);

                cmdItem.ExecuteNonQuery();

                // baixar estoque
                string sqlEstoque = @"UPDATE estoque
                                    SET quantidade = quantidade - @quantidade
                                    WHERE produto_id = @produto_id AND loja_id = @loja_id";

                var cmdEstoque = new MySqlCommand(sqlEstoque, conn, transaction);

                cmdEstoque.Parameters.AddWithValue("@quantidade", item.quantidade);
                cmdEstoque.Parameters.AddWithValue("@produto_id", item.produtoId);
                cmdEstoque.Parameters.AddWithValue("@loja_id", lojaId);

                cmdEstoque.ExecuteNonQuery();
            }

            // pagamento
            string sqlPagamento = @"INSERT INTO pagamentos (venda_id, forma_pagamento, valor)
                                    VALUES (@venda_id, @forma, @valor)";

            var cmdPagamento = new MySqlCommand(sqlPagamento, conn, transaction);

            cmdPagamento.Parameters.AddWithValue("@venda_id", vendaId);
            cmdPagamento.Parameters.AddWithValue("@forma", formaPagamento);
            cmdPagamento.Parameters.AddWithValue("@valor", total);

            cmdPagamento.ExecuteNonQuery();

            transaction.Commit();

            Console.WriteLine("Venda realizada com sucesso!");

        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine("Erro na venda: " + ex.Message);
        }


    }

    public void FinalizarVendaPorCarrinho(int carrinhoId, int lojaId, string formaPagamento)
    {
        using var conn = database.GetConnection();
        conn.Open();

        // pegar cliente
        string sqlCliente = "SELECT cliente_id FROM carrinhos WHERE id=@id";
        var cmdCliente = new MySqlCommand(sqlCliente, conn);
        cmdCliente.Parameters.AddWithValue("@id", carrinhoId);

        int clienteId = Convert.ToInt32(cmdCliente.ExecuteScalar());

        // pegar itens
        string sqlItens = "SELECT produto_id, quantidade FROM itens_carrinho WHERE carrinho_id=@id";
        var cmdItens = new MySqlCommand(sqlItens, conn);
        cmdItens.Parameters.AddWithValue("@id", carrinhoId);

        var reader = cmdItens.ExecuteReader();

        var itens = new List<(int, decimal)>();

        while (reader.Read())
        {
            itens.Add((
                Convert.ToInt32(reader["produto_id"]),
                Convert.ToDecimal(reader["quantidade"])
            ));
        }

        reader.Close();

        // reaproveita método principal
        FinalizarVenda(clienteId, null, lojaId, "online", itens, formaPagamento);

        // limpar carrinho
        string limpar = "DELETE FROM itens_carrinho WHERE carrinho_id=@id";
        var cmdLimpar = new MySqlCommand(limpar, conn);
        cmdLimpar.Parameters.AddWithValue("@id", carrinhoId);
        cmdLimpar.ExecuteNonQuery();
    }

}