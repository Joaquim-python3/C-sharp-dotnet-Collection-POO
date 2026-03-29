using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class CargoRepository
    {
        private Database database;

        public CargoRepository(Database db)
        {
            database = db;
        }

        /// <summary>
        /// Devemos passar o nome do cargo e o id associado ao funcionario
        /// </summary>
        public void CriarCargo(string cargo, int funcionario_id)
        {
            using var conn = database.GetConnection();
            conn.Open();

            string sql = "INSERTO INTO cargos (nome, funcionario_id) VALUES (@cargo, @funcionario_id)";

            var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cargo", cargo);
            cmd.Parameters.AddWithValue("@funcionario_id", funcionario_id);

            cmd.ExecuteNonQuery();
            Console.WriteLine("Cargo criado com sucesso");
        }

        public void ListarCargos()
        {
            using var conn = database.GetConnection().
            conn.Open();

            string sql = "SELECT * FROM cargos";
            var cmd = new MySqlCommand(sql, conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader["id"]} - {reader["nome"]} - {reader["funcionario_id"]}");
            }
        }
        
        /// <summary>
        /// Procurar cargos associado ao id passado
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List<Cargos></returns>
        public List<String> ProcurarCargosPeloIdFuncionario(int id)
        {
            var cargos = new List<string>();

            using var conn = database.GetConnection();
            conn.Open();

            string sql = "SELECT nome FROM cargos WHERE funcionario_id=@id";
            var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cargos.Add(reader["nome"].ToString());
            }

            return cargos;
        }
    }
}