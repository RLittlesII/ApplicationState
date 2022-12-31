using System;
using System.Reactive;

namespace State.Network.Online
{
    public class GoOnline : OnlineHandler
    {
        protected override IObservable<Unit> Handle(GainedSignalEvent state)
        {
            return base.Handle(state);
        }
    }
}