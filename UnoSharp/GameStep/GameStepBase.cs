using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoSharp.GameStep
{
    public abstract class GameStepBase : Samsara, ICommandParser
    {
        public abstract void Parse(Desk desk, Player player, string command);
    }
}
