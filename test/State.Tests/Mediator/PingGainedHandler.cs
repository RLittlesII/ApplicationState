using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Mediator;
using ApplicationState.Network.Online;

namespace State.Tests.Mediator;

internal class PingGainedHandler : ApplicationStateHandler<GainedSignalEvent>, IOnlineHandler
{
    public PingGainedHandler(Results results) => _results = results;

    protected override IObservable<Unit> Handle(GainedSignalEvent applicationStateEvent)
        => Observable.Return(Unit.Default).Finally(() => _results.Messages.Add("Gained Ping"));

    private readonly Results _results;
}