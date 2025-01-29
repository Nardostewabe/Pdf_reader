using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PDF_reader.DatabaseHandler;
using System.Data.SqlClient;

namespace PDF_reader.UserHandler
{
    internal class UserManager
    {
        public string username { get; set; }
        public string password { get; set; }
        public bool RegisterUser(string username, string password)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "INSERT INTO Users (Username, PasswordHash) VALUES (@Username, @PasswordHash)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@PasswordHash", password);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool LoginUser(string username, string password)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT PasswordHash FROM Users WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    return VerifyPassword(password, result.ToString());
                }
                return false;
            }
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            return inputPassword == storedHash;
        }

    }
}
