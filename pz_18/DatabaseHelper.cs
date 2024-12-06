using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_18
{
    public class DatabaseHelper
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDbConnection"].ToString();

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public void ExecuteQuery(string query)
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Пример метода для получения данных
        public void GetData(string query)
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Прочитайте данные, например, через reader.GetString(0) для первого столбца
                }
            }
        }
    }
}
