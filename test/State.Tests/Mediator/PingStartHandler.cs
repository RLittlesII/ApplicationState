using System;
using System.Reactive;
using System.Reactive.Linq;
using State.Application.Foreground;
using State.Mediator;

namespace State.Tests.Mediator;

internal class PingStartHandler : ApplicationStateHandler<StartApplicationEvent>, IForegroundHandler
{
    public PingStartHandler(Results results) => _results = results;

    protected override IObservable<Unit> Handle(StartApplicationEvent applicationStateEvent)
        => Observable.Return(Unit.Default).Finally(() => _results.Messages.Add("Start Ping"));

    private readonly Results _results;
}