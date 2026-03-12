namespace Business;

using MySql.Data.MySqlClient;
using DotNetEnv;

public class Database
{
    /// <summary>
    /// Realiza conexão com o banco de dados
    /// </summary>
    private string connectionString;

    public Database()
    {
        Env.Load();

        //ALTERAR PARA CADA COMPUTADOR
        string? host = Environment.GetEnvironmentVariable("DB_HOST");
        string? port = Environment.GetEnvironmentVariable("3306");
        string? db = Environment.GetEnvironmentVariable("DB_NAME");
        string? user = Environment.GetEnvironmentVariable("DB_USER");
        string? password = Environment.GetEnvironmentVariable("DB_PASSWORD");

        connectionString =
            $"server={host};port={port};database={db};user={user};password={password};";
    }

    /// <summary>
    /// Teste de conexão com o Banco
    /// </summary>
    public void TestConnection()
    {
        using MySqlConnection conn = new MySqlConnection(connectionString);

        conn.Open();
        Console.WriteLine("Conectado ao banco!");
    }
}