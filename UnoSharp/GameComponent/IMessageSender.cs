using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoSharp.GameComponent
{
    public interface IMessageSender
    {
        string Message { get; }
        void AddMessage(string msg);
        void ClearMessage();
    }
}
