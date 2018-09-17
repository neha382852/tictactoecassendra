using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.DatabaseLayer;

namespace TicTacToe
{
    public class AuthorizeService
    {
       public bool IsAuthenticate(string token)
        {
            SQLConnectionEstablisher obj = new SQLConnectionEstablisher();
            SqlConnection connection = obj.createConnection();
            connection.Open();
            string query = "SELECT * from UserDetails where AccessToken= '" +token+"'";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
                return true;
            else
                return false;
        }
    }
}
