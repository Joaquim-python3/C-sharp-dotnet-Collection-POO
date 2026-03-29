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

    }
}