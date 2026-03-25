namespace Business;

using MySql.Data.MySqlClient;
using Domain;
using BCrypt.Net;

public class ClienteRepository
{
    private Database database;

    public ClienteRepository(Database db)
    {
        database = db;
    }

    // CRIAR
    public void CriarCliente(Cliente cliente)
    {
        using var conn = database.GetConnection();
        conn.Open();

        // verificar login existente
        string sql_verificar_login_existente = "SELECT COUNT(*) FROM clientes WHERE login = @login";
        var verificarCmd = new MySqlCommand(sql_verificar_login_existente, conn);
        verificarCmd.Parameters.AddWithValue("@login", cliente.Login);

        int existe_login_cadastrado = Convert.ToInt32(verificarCmd.ExecuteScalar());

        if (existe_login_cadastrado > 0)
        {
            Console.WriteLine("Erro: já existe um cliente com esse login.");
            return;
        }

        // HASH DA SENHA
        string senhaHash = BCrypt.HashPassword(cliente.Senha);

        string sql = "INSERT INTO clientes(nome, email, login, senha) VALUES (@nome, @email, @login, @senha)";

        var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@nome", cliente.Nome);
        cmd.Parameters.AddWithValue("@email", cliente.Email);
        cmd.Parameters.AddWithValue("@login", cliente.Login);
        cmd.Parameters.AddWithValue("@senha", senhaHash);

        cmd.ExecuteNonQuery();

        Console.WriteLine("Cliente criado com sucesso!");
    }

    // READ
    public void ListarClientes()
    {
        using var conn = database.GetConnection();
        conn.Open();

        string sql = "SELECT * FROM clientes";

        var cmd = new MySqlCommand(sql, conn);
        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["id"]} - {reader["nome"]} - {reader["email"]} - {reader["senha"]}");
        }
    }

    // UPDATE
    public void AtualizarCliente(int id, Cliente cliente)
    {
        using var conn = database.GetConnection();
        conn.Open();

        string sql = "UPDATE clientes SET nome=@nome, email=@email WHERE id=@id";

        var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@nome", cliente.Nome);
        cmd.Parameters.AddWithValue("@email", cliente.Email);

        cmd.ExecuteNonQuery();
    }

    // DELETE
    public void DeletarCliente(int id)
    {
        using var conn = database.GetConnection();
        conn.Open();

        string sql = "DELETE FROM clientes WHERE id=@id";

        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

    // FIND BY EMAIL
    // ainda nao retorna nada, apenas imprime
    public Cliente? ProcurarClientePeloEmail(string email)
    {
        using var conn = database.GetConnection();
        conn.Open();

        string sql = "SELECT * FROM clientes WHERE email=@email";

        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@email", email);
        var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Cliente cliente = new Cliente
            {
                id = Convert.ToInt32(reader["id"]),
                Nome = reader["nome"].ToString(),
                Email = reader["email"].ToString(),
                Login = reader["login"]?.ToString(),
                Senha = reader["senha"].ToString()
            };
            return cliente;
        }
        return null;
    }
    
}