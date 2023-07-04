using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService
    {
        private static string db_source = "appserver30003.database.windows.net";
        private static string db_user = "taowutage";
        private static string db_password = "Llf199685!@#";
        private static string db_database = "AppDB";

        private SqlConnection GetCoonnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;
            return new SqlConnection( _builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetCoonnection();
            List<Product> _product_list = new List<Product>();
            string statement = "SELECT PRODUCTID, PRODUCTNAME, QUANTITY FROM PRODUCTS";
            conn.Open();
            SqlCommand cmd = new SqlCommand(statement, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };

                    _product_list.Add(product);
                }

            }
            conn.Close();
            return _product_list;
        }
    }
}
