using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoSharp.GameStep
{
    public interface ICommandParser
    {
        void Parse(Desk desk, Player player, string command);
    }
}
