using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Application.Background;

namespace ApplicationState.Mediator.Tests.Handlers;

internal class PingStopHandler : ApplicationStateHandler<StopApplicationEvent>, IBackgroundHandler
{
    public PingStopHandler(Logger logger) => _logger = logger;

    protected override IObservable<Unit> Handle(StopApplicationEvent applicationStateEvent)
        => Observable.Return(Unit.Default).Finally(() => _logger.Messages.Add("Stop Ping"));

    private readonly Logger _logger;
}