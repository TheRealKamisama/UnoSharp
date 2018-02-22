using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoSharp.GameStep
{
    public class GamingParser : GameStepBase
    {
        public override void Parse(Desk desk, Player player, string command)
        {
            if (!IsValidPlayer(desk, player)) return;
            
            if (command == "摸" || command == "摸牌" || command == "mo" || command == "draw")
            {
                MoveNext(desk);
                player.Cards.Add(Card.Generate());
                player.Cards.Sort();
                player.SendCardsMessage();
                desk.AddMessageLine("已为你摸牌.");
                desk.SendLastcardMessage();
                return;
            }

            var card = command.ToCard();
            if (card == null)
            {
                desk.AddMessage("无法匹配你想出的牌.");
                return;
            }

            if (!UnoRule.IsValid(card, desk.LastCard))
            {
                desk.AddMessage("你想出的牌并不匹配 UNO 规则.");
                return;
            }

            if (!card.IsValidForPlayerAndRemove(player))
            {
                desk.AddMessage("你的手里并没有这些牌.");
                return;
            }

            desk.LastCard = card;

            if (player.Cards.Count == 0)
            {
                desk.FinishGame(player);
                return;
            }
            Behave(desk, player, card);
            desk.SendLastcardMessage();
        }

        private void Behave(Desk desk, Player player, Card card)
        {
            player.SendCardsMessage();
            MoveNext(desk);
            var nextPlayer = desk.CurrentPlayer;
            switch (card.Type)
            {
                case CardType.Number:
                    // ignored
                    break;
                case CardType.Reverse:
                    desk.AddMessage($"方向反转.");
                    Reverse();
                    MoveNext(desk);
                    MoveNext(desk);
                    break;
                case CardType.Skip:
                    desk.AddMessage($"{nextPlayer.ToAtCode()}被跳过.");
                    MoveNext(desk);
                    nextPlayer = desk.CurrentPlayer;
                    break;
                case CardType.DrawTwo:
                    desk.AddMessage($"{nextPlayer.ToAtCode()}被罚加2张牌.");
                    desk.State = GamingState.WaitingDrawTwoOverlay;
                    desk.OverlayCardNum = 2;
                    BehaveDrawTwo(nextPlayer, desk);
                    //nextPlayer.Cards.AddRange(Card.Generate(2));
                    //nextPlayer.Cards.Sort();
                    //nextPlayer.SendCardsMessage();
                    //MoveNext(desk);
                    break;
                case CardType.Wild:
                    desk.AddMessage($"变色");
                    break;
                case CardType.DrawFour:
                    desk.AddMessage($"{nextPlayer.ToAtCode()}被罚加4张牌."); // TODO
                    desk.State = GamingState.WaitingDrawFourOverlay;
                    desk.OverlayCardNum = 4;
                    BehaveDrawFour(nextPlayer, desk);
                    //nextPlayer.Cards.AddRange(Card.Generate(4));
                    //nextPlayer.Cards.Sort();
                    //nextPlayer.SendCardsMessage();
                    //MoveNext(desk);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private void BehaveDrawFour(Player nextPlayer, Desk desk)
        {
            
        }

        private void BehaveDrawTwo(Player nextPlayer, Desk desk)
        {
            
        }
    }
}
