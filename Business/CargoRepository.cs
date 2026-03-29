using System;
using System.Collections.Generic;
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

    }
}