using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Application.Foreground;

namespace ApplicationState.Mediator.Tests.Handlers;

internal class PingStartHandler : ApplicationStateHandler<StartApplicationEvent>, IForegroundHandler
{
    public PingStartHandler(Logger logger) => _logger = logger;

    protected override IObservable<Unit> Handle(StartApplicationEvent applicationStateEvent)
        => Observable.Return(Unit.Default).Finally(() => _logger.Messages.Add("Start Ping"));

    private readonly Logger _logger;
}