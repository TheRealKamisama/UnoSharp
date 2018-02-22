using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoSharp.GameComponent;

namespace UnoSharp
{
    public class Player : MessageSenderBase
    {
        public Player(string playerId)
        {
            PlayerId = playerId;
        }
        public string PlayerId { get; }
        public List<Card> Cards { get; } = new List<Card>();
        public bool SaidUNO { get; set; }
        public DateTime SaidUnoTime { get; }

        public virtual string ToAtCode()
        {
#if !DEBUG
            return $"[CQ:at,qq={PlayerId}]";
#else
            return $"{PlayerId}";
#endif
        }
        
        public void SendCardsMessage()
        {
            AddMessage(Cards.ToImage().ToImageCode());
        }
    }
}
