using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Application.Foreground;
using ApplicationState.Mediator;

namespace State.Tests.Mediator;

internal class PongResumeHandler : ApplicationStateHandler<ResumeApplicationEvent>, IResumeHandler
{
    public PongResumeHandler(Results results) => _results = results;

    protected override IObservable<Unit> Handle(ResumeApplicationEvent applicationStateEvent)
        => Observable.Return(Unit.Default).Finally(() => _results.Messages.Add("Resume Pong"));

    private readonly Results _results;
}