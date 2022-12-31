using System;
using System.Reactive;
using System.Reactive.Linq;
using State.Application.Foreground;
using State.Mediator;

namespace State.Tests.Mediator;

internal class PingResumeHandler : ApplicationStateHandler<ResumeApplicationEvent>, IResumeHandler
{
    public PingResumeHandler(Results results) => _results = results;

    protected override IObservable<Unit> Handle(ResumeApplicationEvent applicationStateEvent)
        => Observable.Return(Unit.Default).Finally(() => _results.Messages.Add("Resume Ping"));

    private readonly Results _results;
}