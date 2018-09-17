using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Model;
using Cassandra;

namespace TicTacToe.DatabaseLayer
{
    public class SQLRepository : IRepository
    {
        public string InsertIntoDatabase(User userObject)
        {
            /*
            SQLConnectionEstablisher obj = new SQLConnectionEstablisher();
            SqlConnection connection = obj.createConnection();
            connection.Open();
            string Username = null;
            string response = null;
            string query = "Select * from UserDetails where Username=@Username";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(new SqlParameter("@Username", userObject.Username));
            using (SqlDataReader datareader = cmd.ExecuteReader())
            {
                while (datareader.Read())
                {
                    Username = datareader["Username"].ToString();
                }
            }
            if (Username == null)
            {

                string token = Guid.NewGuid().ToString();
                userObject.AccessToken = token;
                query = "insert into UserDetails values(@Fname,@Lname,@Username,@AccessToken)";
                cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add(new SqlParameter("@Fname", userObject.Fname));
                cmd.Parameters.Add(new SqlParameter("@Lname", userObject.Lname));
                cmd.Parameters.Add(new SqlParameter("@Username", userObject.Username));
                cmd.Parameters.Add(new SqlParameter("@AccessToken", userObject.AccessToken));
                cmd.ExecuteNonQuery();
                response = "User has been Successfully added";
                connection.Close();
            }
          
            return response;*/
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("tictactoe");
            string token = Guid.NewGuid().ToString();
            userObject.AccessToken = token;
            //Prepare a statement once
            var ps = session.Prepare("INSERT INTO userdetails (username ,accesstoken, fname ,lname) VALUES (?,?,?,?)");

            //...bind different parameters every time you need to execute
            var statement = ps.Bind(userObject.Username, userObject.AccessToken, userObject.Fname, userObject.Lname );
            //Execute the bound statement with the provided parameters
            session.Execute(statement);
            return "User has been Successfully added";
        }

        public string RetrieveFromDatabase(string username)
        {
            /*
            string token = null;
            SQLConnectionEstablisher obj = new SQLConnectionEstablisher();
            SqlConnection connection = obj.createConnection();
            connection.Open();
            string query = "select * from UserDetails where Id= @id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            SqlDataReader datareader = cmd.ExecuteReader();
            while (datareader.Read())
            {
                token = datareader["AccessToken"].ToString();
            }

            return token;
            */
            //Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            //ISession session = cluster.Connect("tictactoe");
            //Row result = session.Execute("select * from userdetails where username=" + username + "").First();

            // Connect to the TicTacToe keyspace on our cluster running at 127.0.0.1
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("tictactoe");
            Row result = session.Execute("select * from userdetails where username=" + username + "").First();
            string currentUserToken = result["accesstoken"].ToString();
            return currentUserToken;

        }


    }

}
