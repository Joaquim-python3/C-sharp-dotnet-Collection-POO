namespace Business;

using MySql.Data.MySqlClient;
using Domain;

public class FuncionarioRepository
{
    private Database database;

    public FuncionarioRepository(Database db)
    {
        database = db;
    }

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
}