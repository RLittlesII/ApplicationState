using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Machine.Background;

namespace ApplicationState.Mediator.Tests.Handlers;

internal class PongStopHandler : ApplicationStateHandler<StopApplicationEvent>, IBackgroundHandler
{
    public PongStopHandler(Logger logger) => _logger = logger;

    protected override IObservable<Unit> Handle(StopApplicationEvent applicationStateEvent)
        => Observable.Return(Unit.Default).Finally(() => _logger.Messages.Add("Stop Pong"));

    private readonly Logger _logger;
}