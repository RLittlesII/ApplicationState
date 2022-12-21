using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Mediator;
using ApplicationState.Network.Offline;

namespace State.Tests.Mediator;

internal class PingLostHandler : ApplicationStateHandler<LostSignalEvent>, IOfflineHandler
{
    public PingLostHandler(Results results) => _results = results;

    protected override IObservable<Unit> Handle(LostSignalEvent applicationStateEvent)
        => Observable.Return(Unit.Default).Finally(() => _results.Messages.Add("Lost Ping"));

    private readonly Results _results;
}