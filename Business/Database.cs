namespace Business;

using MySql.Data.MySqlClient;
using DotNetEnv;

    /// <summary>
    /// Variáveis e Funções para conexão com o banco de dados
    /// </summary>
public class Database
{
    private string connectionString;

    public Database()
    {
        Env.Load();

        //ALTERAR PARA CADA COMPUTADOR
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
    /// 
    /// </summary>
    /// <returns>String de conexão com MySQL</returns>
    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
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