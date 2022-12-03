using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Machine.Network.Online;

namespace ApplicationState.Mediator.Tests.Handlers;

internal class PingGainedHandler : ApplicationStateHandler<GainedSignalEvent>, IOnlineHandler
{
    public PingGainedHandler(Logger logger) => _logger = logger;

    protected override IObservable<Unit> Handle(GainedSignalEvent applicationStateEvent)
        => Observable.Return(Unit.Default).Finally(() => _logger.Messages.Add("Gained Ping"));

    private readonly Logger _logger;
}