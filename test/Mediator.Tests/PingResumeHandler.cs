using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Machine.Foreground;

namespace ApplicationState.Mediator.Tests.Handlers;

internal class PingResumeHandler : ApplicationStateHandler<ResumeApplicationEvent>, IResumeHandler
{
    public PingResumeHandler(Logger logger) => _logger = logger;

    protected override IObservable<Unit> Handle(ResumeApplicationEvent applicationStateEvent)
        => Observable.Return(Unit.Default).Finally(() => _logger.Messages.Add("Resume Ping"));

    private readonly Logger _logger;
}