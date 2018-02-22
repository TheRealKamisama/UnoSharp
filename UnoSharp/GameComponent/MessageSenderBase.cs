using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UnoSharp.GameComponent
{
    public abstract class MessageSenderBase : IMessageSender
    {
        private readonly object _locker = new object();
        public string Message { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void AddMessage(string msg)
        {
            lock (_locker) {
                Message += msg;
            }
        }

        public void ClearMessage()
        {
            lock (_locker) {
                Message = null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddMessageLine(string msg = "")
        {
            if (!Message?.EndsWith(Environment.NewLine) == true) {
                AddMessage(Environment.NewLine);
            }
            AddMessage(msg + Environment.NewLine);
        }
    }
}
