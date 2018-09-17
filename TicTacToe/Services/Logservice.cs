using Cassandra;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.DatabaseLayer;
using TicTacToe.Model;

namespace TicTacToe
{
    public class Logservice
    {
        public void Add(Logger1 logObject)
        {
            /*
            SQLConnectionEstablisher obj = new SQLConnectionEstablisher();
            SqlConnection connection = obj.createConnection();
            SqlCommand cmd;
            string query;
            connection.Open();
            query = "insert into LogDetails values(@Request,@Response,@Exception)";
            cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(new SqlParameter("@Request", logObject.Request));
            cmd.Parameters.Add(new SqlParameter("@Response", logObject.Response));
            cmd.Parameters.Add(new SqlParameter("@Exception", logObject.Exception));
            cmd.ExecuteNonQuery();
            connection.Close();
            */
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("tictactoe");
           
            //Prepare a statement once
            var ps = session.Prepare("INSERT INTO logdetails(logid, Exception, Request, Response) VALUES (?,?,?,?)");

            //...bind different parameters every time you need to execute
            var statement = ps.Bind(Guid.NewGuid(), logObject.Exception, logObject.Request, logObject.Response);
            //Execute the bound statement with the provided parameters
            session.Execute(statement);
           
        }
    }
}
