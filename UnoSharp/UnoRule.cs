using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoSharp
{
    public class UnoRule
    {
        public static bool IsValid(Card thisCard, Card lastCard)
        {
            if (thisCard.Type == CardType.DrawFour || thisCard.Type == CardType.Wild) return true;
            return thisCard.Color == lastCard.Color || thisCard.ValueNumber == lastCard.ValueNumber;
        }
    }
}
