using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.DatabaseLayer
{
    public class SQLConnectionEstablisher
    {
        public SqlConnection createConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source=TAVDESK045;Initial Catalog=TicTacToe;User ID=sa;Password=test123!@#";
            return connection;
        }
        
    }
}
