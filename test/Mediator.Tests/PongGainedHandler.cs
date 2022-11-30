using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Machine.Online;

namespace ApplicationState.Mediator.Tests.Handlers;

internal class PongGainedHandler : ApplicationStateHandler<GainedSignalEvent>, IOnlineHandler
{
    public PongGainedHandler(Logger logger) => _logger = logger;

    protected override IObservable<Unit> Handle(GainedSignalEvent applicationStateEvent)
        => Observable.Return(Unit.Default).Finally(() => _logger.Messages.Add("Gained Pong"));

    private readonly Logger _logger;
}