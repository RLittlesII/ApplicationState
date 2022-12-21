using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Application.Foreground;
using ApplicationState.Mediator;

namespace State.Tests.Mediator;

internal class PongStartHandler : ApplicationStateHandler<StartApplicationEvent>, IForegroundHandler
{
    public PongStartHandler(Results results) => _results = results;

    protected override IObservable<Unit> Handle(StartApplicationEvent applicationStateEvent)
        => Observable.Return(Unit.Default).Finally(() => _results.Messages.Add("Start Pong"));

    private readonly Results _results;
}