using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Model;
using TicTacToe.DatabaseLayer;
using TicTacToe.Aspects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicTacToe.Controllers
{
    [Route("api/[controller]")]
    [Log]
    [ExceptionHandler]
    public class IdentityController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{username}")]
        public string Get(string username)
        {
            SQLRepository obj = new SQLRepository();
            string token = obj.RetrieveFromDatabase(username);
            return token;
        }

        // POST api/values
        [HttpPost]
     
        public string Post([FromBody]User userobject)
        {
            SQLRepository obj = new SQLRepository();
           string response = obj.InsertIntoDatabase(userobject);
            if(response == null)
            {
                throw new Exception("User with same name already exists.");
            }
            return response;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
