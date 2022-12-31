using System;
using System.Reactive;
using System.Reactive.Linq;
using State.Mediator;
using State.Network.Offline;

namespace State.Tests.Mediator;

internal class PongLostHandler : ApplicationStateHandler<LostSignalEvent>, IOfflineHandler
{
    public PongLostHandler(Results results) => _results = results;

    protected override IObservable<Unit> Handle(LostSignalEvent applicationStateEvent)
        => Observable.Return(Unit.Default).Finally(() => _results.Messages.Add("Lost Pong"));

    private readonly Results _results;
}