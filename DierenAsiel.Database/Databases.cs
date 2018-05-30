using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel.Database
{
    public static class Databases
    {
        static Databases()
        {

        }

        public static TestDatabaseController testDatabase = new TestDatabaseController();
        public static DatabaseController productionDatabase = new DatabaseController();
    }
}
