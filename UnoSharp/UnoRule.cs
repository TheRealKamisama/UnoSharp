using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoSharp
{
    public class UnoRule
    {
        public static bool IsValid(Card thisCard, Card lastCard, GamingState state)
        {
            switch (state)
            {
                case GamingState.Gaming:
                    if (thisCard.Type == CardType.DrawFour || thisCard.Type == CardType.Wild)
                        return true;
                    return thisCard.Color == lastCard.Color || thisCard.ValueNumber == lastCard.ValueNumber;
                case GamingState.WaitingDrawTwoOverlay:
                    return thisCard.Type == CardType.DrawTwo;
                case GamingState.WaitingDrawFourOverlay:
                    return thisCard.Type == CardType.DrawFour;
                case GamingState.Doubting:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, "Unknown state!");
            }
            
        }
    }
}
