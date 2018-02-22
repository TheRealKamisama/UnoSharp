using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoSharp.GameStep
{
    public class Samsara
    {
        public int CurrentIndex { get; internal set; }
        public bool Reversed { get; protected set; }

        protected bool IsValidPlayer(Desk desk, Player player)
        {
            if (!desk.Players.Contains(player))
                return false;
            return desk.Players.ToList().FindIndex(p => p == player) == CurrentIndex;
        }

        

        public void Reverse()
        {
            Reversed = !Reversed;
        }

        internal virtual void MoveNext(Desk desk)
        {
            if (Reversed)
            {
                CurrentIndex--;
                if (CurrentIndex == -1)
                {
                    CurrentIndex = desk.Players.Count() - 1;
                }
            }
            else
            {
                CurrentIndex = (CurrentIndex + 1) % desk.Players.Count();
            }
        }

        public Player Next(Desk desk)
        {
            var current = CurrentIndex;
            MoveNext(desk);
            var cp = desk.PlayerList[CurrentIndex];
            CurrentIndex = current;
            return cp;
        }

        public Player Previous(Desk desk)
        {
            var current = CurrentIndex;
            Reverse();
            MoveNext(desk);
            var cp = desk.PlayerList[CurrentIndex];
            Reverse();
            CurrentIndex = current;
            return cp;
        }
    
}
}
