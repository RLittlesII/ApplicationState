using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Application.Initialize;

namespace State.Tests.Mediator
{
    internal class TestInitializeHandler : InitializationHandler, IInitializationHandler
    {
        public TestInitializeHandler(Results results) => _results = results;

        protected override IObservable<Unit> Handle(InitializeApplicationEvent applicationStateEvent)
            => Observable.Return(Unit.Default).Finally(() => _results.Messages.Add("Initialized"));

        private readonly Results _results;
    }
}