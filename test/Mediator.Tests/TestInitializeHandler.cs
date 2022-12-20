using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Application.Initialize;

namespace ApplicationState.Mediator.Tests.Handlers
{
    internal class TestInitializeHandler : InitializationHandler, IInitializationHandler
    {
        public TestInitializeHandler(Logger logger) => _logger = logger;

        protected override IObservable<Unit> Handle(InitializeApplicationEvent applicationStateEvent)
            => Observable.Return(Unit.Default).Finally(() => _logger.Messages.Add("Initialized"));

        private readonly Logger _logger;
    }
}