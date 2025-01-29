using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PDF_reader.DatabaseHandler
{
    internal class DatabaseHelper
    {
        private static readonly string connectionString = "Server=NARDOS;Database=MyPDFs;Integrated Security=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
