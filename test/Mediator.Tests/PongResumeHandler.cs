using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Machine.Application.Foreground;

namespace ApplicationState.Mediator.Tests.Handlers;

internal class PongResumeHandler : ApplicationStateHandler<ResumeApplicationEvent>, IResumeHandler
{
    public PongResumeHandler(Logger logger) => _logger = logger;

    protected override IObservable<Unit> Handle(ResumeApplicationEvent applicationStateEvent)
        => Observable.Return(Unit.Default).Finally(() => _logger.Messages.Add("Resume Pong"));

    private readonly Logger _logger;
}