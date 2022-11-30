using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Machine.Foreground;

namespace ApplicationState.Mediator.Tests.Handlers;

internal class PongStartHandler : ApplicationStateHandler<StartApplicationEvent>, IForegroundHandler
{
    public PongStartHandler(Logger logger) => _logger = logger;

    protected override IObservable<Unit> Handle(StartApplicationEvent applicationStateEvent)
        => Observable.Return(Unit.Default).Finally(() => _logger.Messages.Add("Start Pong"));

    private readonly Logger _logger;
}