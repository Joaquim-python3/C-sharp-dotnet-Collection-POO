namespace Business;

using MySql.Data.MySqlClient;
using Domain;
using Domain.Services;

public class FuncionarioRepository
{
    private Database database;

    public FuncionarioRepository(Database db)
    {
        database = db;
    }

    // Criar funcionarios
    public void CriarFuncionario(Funcionario f)
    {
        using var conn = database.GetConnection();
        conn.Open();

        string sql = @"INSERT INTO funcionarios 
        (nome, salario, hora_entrada, hora_saida, regime_contratual, loja_id)
        VALUES (@nome, @salario, @entrada, @saida, @regime, @lojaId);
        SELECT LAST_INSERT_ID();";

        var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@nome", f.Nome);
        cmd.Parameters.AddWithValue("@salario", f.Salario);
        cmd.Parameters.AddWithValue("@entrada", f.HoraEntrada);
        cmd.Parameters.AddWithValue("@saida", f.HoraSaida);
        cmd.Parameters.AddWithValue("@regime", f.RegimeContratual.ToString());
        cmd.Parameters.AddWithValue("@lojaId", f.LojaId);

        int funcionarioId = Convert.ToInt32(cmd.ExecuteScalar()); // 🔥 AQUI

        // Inserir cargos
        foreach (var cargo in f.Cargos)
        {
            string sqlCargo = @"INSERT INTO cargos (nome, funcionario_id)
                                VALUES (@nome, @funcionarioId)";

            var cmdCargo = new MySqlCommand(sqlCargo, conn);

            cmdCargo.Parameters.AddWithValue("@nome", cargo.ToString().ToLower());
            cmdCargo.Parameters.AddWithValue("@funcionarioId", funcionarioId);

            cmdCargo.ExecuteNonQuery();
        }
    }

    // Listar Funcionarios
    public void ListarFuncionarios()
    {
        List<Funcionario> funcionarios = new List<Funcionario>();

        using var conn = database.GetConnection();
        conn.Open();

        string sql = "SELECT * FROM funcionarios";

        var cmd = new MySqlCommand(sql, conn);
        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(@$"{reader["id"]} - {reader["nome"]} - {reader["salario"]} - ENTRADA: {reader["hora_entrada"]} - SAIDA: {reader["hora_saida"]} - TIPO DE CONTRATO: {reader["regime_contratual"]}");
        }
    }

    // Deletar Fucionario
    public void DeletarFuncionario(int id)
    {
        using var conn = database.GetConnection();
        conn.Open();

        // deleta o cargo primeiro
        string sqlCargos = "DELETE FROM cargos WHERE funcionario_id = @id";
        var cmdCargos = new MySqlCommand(sqlCargos, conn);
        cmdCargos.Parameters.AddWithValue("@id", id);
        cmdCargos.ExecuteNonQuery();

        // deleta o funcionario
        string sql = "DELETE FROM funcionarios WHERE id=@id";
        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    // update
    public void AtualizarFuncionario(Funcionario funcionario)
    {
        // faço uma cópia da referencia do funcionario
        FuncionarioService funcionarioService = new FuncionarioService();
        Funcionario novo_funcionario = funcionarioService.AtualizarFuncionario(funcionario);

        // deleto o funcionario antigo
        DeletarFuncionario(funcionario.id);

        // substituir pelo o novo funcionario
        CriarFuncionario(novo_funcionario);

    }

    // Procurar pelo id
    public Funcionario FuncionarioPeloId(int id)
    {
        using var conn = database.GetConnection();
        conn.Open();

        string sql = "SELECT * FROM funcionarios WHERE id=@id";

        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);

        var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Funcionario funcionario = new Funcionario
            {
                id = Convert.ToInt32(reader["id"]),
                Nome = reader["nome"].ToString(),
                Salario = Convert.ToDecimal(reader["salario"]),
                HoraEntrada = Convert.ToDateTime(reader["hora_entrada"]),
                HoraSaida = Convert.ToDateTime(reader["hora_saida"]),
                RegimeContratual = reader["regime_contratual"].ToString()
            };

            return funcionario;
        }

        return null;
    }
}