using System;
using System.Reactive;
using System.Reactive.Linq;
using State.Mediator;
using State.Network.Online;

namespace State.Tests.Mediator;

internal class PongGainedHandler : ApplicationStateHandler<GainedSignalEvent>, IOnlineHandler
{
    public PongGainedHandler(Results results) => _results = results;

    protected override IObservable<Unit> Handle(GainedSignalEvent applicationStateEvent)
        => Observable.Return(Unit.Default).Finally(() => _results.Messages.Add("Gained Pong"));

    private readonly Results _results;
}