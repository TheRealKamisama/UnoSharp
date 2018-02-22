using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Newbe.Mahua;
using UnoSharp;

namespace Origind.Card.Uno.MahuaEvents
{
    public class TimerEvent : ITimerEvent
    {
        private readonly IMahuaApi _api;
        private readonly Timer _timer;

        public TimerEvent(IMahuaApi api)
        {
            _api = api;
            _timer = new Timer(100);
            var timer = _timer;
            timer.Elapsed += (sender, args) =>
            {
                foreach (var desk in Desk.GetDesks())
                {
                    if (desk.Message != null)
                    {
                        var msg = desk.Message;
                        desk.ClearMessage();
                        _api.SendGroupMessage(desk.DeskId, msg);
                    }

                    foreach (var player in desk.Players.Where(player => player.Message != null))
                    {
                        var msg = player.Message;
                        player.ClearMessage();
                        _api.SendPrivateMessage(player.PlayerId, msg);
                    }
                }
            };
        }

        public void Start()
        {
            _timer.Start();
        }
    }
    
}
