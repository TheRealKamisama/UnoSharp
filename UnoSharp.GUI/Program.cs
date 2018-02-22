using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoSharp;

namespace UnoSharp.GUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            var desk = new Desk("test");
            var players = new List<Player>
            {
                new Player("1", desk) {Cards =
                {
                    new Card(CardValue.Eight, CardColor.Blue),
                    new Card(CardValue.Eight, CardColor.Blue),
                    new Card(CardValue.Eight, CardColor.Blue),
                    new Card(CardValue.Eight, CardColor.Blue),
                    new Card(CardValue.Eight, CardColor.Blue),
                }},
                new Player("2", desk) {Cards =
                {
                    new Card(CardValue.Eight, CardColor.Blue),
                    new Card(CardValue.Eight, CardColor.Blue),
                    new Card(CardValue.Eight, CardColor.Blue),

                }},
                new Player("3", desk){Cards =
                {
                    new Card(CardValue.Eight, CardColor.Blue),

                }},
                new Player("4", desk){Cards =
                {
                    new Card(CardValue.Eight, CardColor.Blue),
                    new Card(CardValue.Eight, CardColor.Blue),
                    new Card(CardValue.Eight, CardColor.Blue),
                    new Card(CardValue.Eight, CardColor.Blue),
                    new Card(CardValue.Eight, CardColor.Blue),
                    new Card(CardValue.Eight, CardColor.Blue),

                }},
                new Player("5", desk){Cards =
                {

                    new Card(CardValue.Eight, CardColor.Blue),

                }},
            };
            desk.LastCard = Card.Generate();
            foreach (var player in players)
            {
                desk.AddPlayer(player);
            }
            desk.StartGame();
            //desk._currentParser.CurrentIndex = 2;
            desk.ParseMessage("1","摸");
            DeskRenderer.RenderDesk(desk).Save("test3.png");
            //desk.Reversed = true;
            DeskRenderer.RenderDesk(desk).Save("test4.png");
            
            //DeskRenderer.RenderDesk(desk).Save("test5.png");
        }
    }
}
