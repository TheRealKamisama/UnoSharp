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
            var cards = Card.CardsPool.ToList();
            cards.Sort();
            cards.ToImage().Save("test.png");
        }
    }
}
