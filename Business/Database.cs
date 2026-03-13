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
        string? host = "localhost";
        string? port = "3306";
        string? db = "loja";
        string? user = "root";
        string? password = "root";

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