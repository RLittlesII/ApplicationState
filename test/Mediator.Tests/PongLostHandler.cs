using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Machine.Offline;

namespace ApplicationState.Mediator.Tests.Handlers;

internal class PongLostHandler : ApplicationStateHandler<LostSignalEvent>, IOfflineHandler
{
    public PongLostHandler(Logger logger) => _logger = logger;

    protected override IObservable<Unit> Handle(LostSignalEvent applicationStateEvent)
        => Observable.Return(Unit.Default).Finally(() => _logger.Messages.Add("Lost Pong"));

    private readonly Logger _logger;
}