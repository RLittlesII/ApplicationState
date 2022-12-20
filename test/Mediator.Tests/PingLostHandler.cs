using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Network.Offline;

namespace ApplicationState.Mediator.Tests.Handlers;

internal class PingLostHandler : ApplicationStateHandler<LostSignalEvent>, IOfflineHandler
{
    public PingLostHandler(Logger logger) => _logger = logger;

    protected override IObservable<Unit> Handle(LostSignalEvent applicationStateEvent)
        => Observable.Return(Unit.Default).Finally(() => _logger.Messages.Add("Lost Ping"));

    private readonly Logger _logger;
}