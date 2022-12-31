using System;
using System.Reactive;
using System.Reactive.Linq;
using State.Application.Background;
using State.Mediator;

namespace State.Tests.Mediator;

internal class PongStopHandler : ApplicationStateHandler<StopApplicationEvent>, IBackgroundHandler
{
    public PongStopHandler(Results results) => _results = results;

    protected override IObservable<Unit> Handle(StopApplicationEvent applicationStateEvent)
        => Observable.Return(Unit.Default).Finally(() => _results.Messages.Add("Stop Pong"));

    private readonly Results _results;
}