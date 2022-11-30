using System;
using System.Reactive;

namespace ApplicationState.Machine.Online
{
    public class GoOnline : OnlineHandler
    {
        protected override IObservable<Unit> Handle(GainedSignalEvent state)
        {
            return base.Handle(state);
        }
    }
}