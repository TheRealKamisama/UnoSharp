using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoSharp.GameStep
{
    public class WaitingParser : GameStepBase
    {
        public override void Parse(Desk desk, Player player, string command)
        {
            switch (command.ToUpper())
            {
                case "加入UNO":
                    desk.AddPlayer(player);
                    break;
                case "离开UNO":
                    desk.RemovePlayer(player);
                    break;
                case "开始UNO":
                    desk.StartGame();
                    break;
            }

        }
    }
}
