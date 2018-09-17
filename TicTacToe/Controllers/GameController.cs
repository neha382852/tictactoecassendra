using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TicTacToe.Aspects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicTacToe.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [Log]
    [ExceptionHandler]
    public class GameController : Controller
    {
        //valid box-ids are:- 1,2,3,4,5,6,7,8,9
        static List<int> blockedList = new List<int>();
        static List<string> UsernameList = new List<string>();
        static List<int> player1list = new List<int>();
        static List<int> player2list = new List<int>();
       static bool IsPlayer1 = true;
       static bool IsPlayer2 = true;
       static string player1, player2;
       static int countmoves = 0;
        static int flag = 0;
        string result = null;
        // GET: api/values/5
        [HttpGet]
        public string Get(int id)
        {
            return "value1";
        }
        static string checkWinner()
        {
            if (player1list.Contains(1) && player1list.Contains(2) && player1list.Contains(3) || player1list.Contains(4) && player1list.Contains(5) && player1list.Contains(6) || player1list.Contains(7) && player1list.Contains(8) && player1list.Contains(9))
                return "Player1 is the winner";
            else if (player1list.Contains(1) && player1list.Contains(4) && player1list.Contains(7) || player1list.Contains(2) && player1list.Contains(5) && player1list.Contains(8) || player1list.Contains(3) && player1list.Contains(6) && player1list.Contains(9))
                return "Player1 is the winner";
            else if (player1list.Contains(1) && player1list.Contains(5) && player1list.Contains(9) || player1list.Contains(3) && player1list.Contains(5) && player1list.Contains(7))
                return "Player1 is the winner";
            else if (player2list.Contains(1) && player2list.Contains(2) && player2list.Contains(3) || player2list.Contains(4) && player2list.Contains(5) && player2list.Contains(6) || player2list.Contains(7) && player2list.Contains(8) && player2list.Contains(9))
                return "Player2 is the winner";
            else if (player2list.Contains(1) && player2list.Contains(4) && player2list.Contains(7) || player2list.Contains(2) && player2list.Contains(5) && player2list.Contains(8) || player2list.Contains(3) && player2list.Contains(6) && player2list.Contains(9))
                return "Player2 is the winner";
            else if (player2list.Contains(1) && player2list.Contains(5) && player2list.Contains(9) || player2list.Contains(3) && player2list.Contains(5) && player2list.Contains(7))
                return "Player2 is the winner";
            else
            {
                if (countmoves == 9)
                    return "Draw";

            }
           
            return "In Progress";

        }
        // GET api/values
        [HttpGet("{id}")]
        public string Get()
        {
            string winner = checkWinner();
            if (countmoves == 9 && winner == "Progress")
                return "Draw";
            return winner;
         
        
        }

        // POST api/values
        [HttpPost]
     
        public string Post([FromHeader]string username,[FromHeader]string Boxid)
        {
           int BoxId = int.Parse(Boxid);
           
               
                     if (UsernameList.Count < 2)
                     {
                         UsernameList.Add(username);
                     }

            if (!UsernameList.Contains(username))
            {
                throw new Exception("This is a 2-player game");
            }
            
            if (flag == 0)
            {
                if (UsernameList.Count == 1)
                {
                    player1 = username;
                }
                if (UsernameList.Count == 2)
                {
                    player2 = username;
                    flag = 1;

                }
            }

            if (UsernameList.Count <= 2)
            {
                if (BoxId < 1 && BoxId > 9)
                {
                    throw new Exception("Invalid box-id");
                }

                if (IsPlayer1)
                {
                    if (player1 == username)
                    {
                       
                        if (!blockedList.Contains(BoxId))
                        {
                            IsPlayer1 = false;
                            IsPlayer2 = true;
                            blockedList.Add(BoxId);
                            player1list.Add(BoxId);
                            countmoves++;
                            result = checkWinner();

                        }
                        else
                        {
                            throw new Exception("This position is already taken.");
                        }
                    }
                    else
                    {
                        throw new Exception("Alternate user needs to play.");
                    }
                }
                else if (IsPlayer2)
                {
                    if (player2 == username)
                    {
                      
                        if (!blockedList.Contains(BoxId))
                        {
                            IsPlayer2 = false;
                            IsPlayer1 = true;
                            blockedList.Add(BoxId);
                            player2list.Add(BoxId);
                            countmoves++;
                            result = checkWinner();
                        }
                        else
                        {
                            throw new Exception("This position is already taken.");
                        }
                    }
                    else
                    {
                        throw new Exception("Alternate user needs to play.");
                    }
                }
             

            }
           if(result== "Player1 is the winner" || result== "Player2 is the winner"|| result=="Draw")
            {
                result = result + " .Game is over now.You can start new game.";
                blockedList.Clear();
                player2list.Clear();
                player1list.Clear();
                UsernameList.Clear();
                IsPlayer1 = true;
                IsPlayer2 = true;
                countmoves = 0;
                flag = 0;
                
            }
            return result;
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
