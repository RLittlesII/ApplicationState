using System;
using System.Reactive;

namespace ApplicationState.Network.Online
{
    public class GoOnline : OnlineHandler
    {
        protected override IObservable<Unit> Handle(GainedSignalEvent state)
        {
            return base.Handle(state);
        }
    }
}