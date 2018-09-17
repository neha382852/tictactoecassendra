using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Model
{
    public class User
    {
        public int id { get; set; }

        public string Fname { get; set; }

        public string Lname { get; set; }

        public string Username { get; set; }

        public string AccessToken { get; set; }
    }
}
