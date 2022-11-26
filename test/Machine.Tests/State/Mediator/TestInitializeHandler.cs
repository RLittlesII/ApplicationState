using ApplicationState.Machine.Events;
using ApplicationState.Machine.Handlers;
using ApplicationState.Mediator;

namespace ApplicationState.Machine.Tests.State.Mediator
{
    internal class TestInitializeHandler : InitializationHandler, IInitializationHandler
    {
        public TestInitializeHandler(Logger logger) => _logger = logger;

        protected override IObservable<Unit> Handle(InitializeApplicationEvent applicationStateEvent)
            => Observable.Return(Unit.Default).Finally(() => _logger.Messages.Add("Initialized"));

        private readonly Logger _logger;
    }

    internal class PingStartHandler : ApplicationStateHandler<StartApplicationEvent>, IForegroundHandler
    {
        public PingStartHandler(Logger logger) => _logger = logger;

        protected override IObservable<Unit> Handle(StartApplicationEvent applicationStateEvent)
            => Observable.Return(Unit.Default).Finally(() => _logger.Messages.Add("Start Ping"));

        private readonly Logger _logger;
    }

    internal class PongStartHandler : ApplicationStateHandler<StartApplicationEvent>, IForegroundHandler
    {
        public PongStartHandler(Logger logger) => _logger = logger;

        protected override IObservable<Unit> Handle(StartApplicationEvent applicationStateEvent)
            => Observable.Return(Unit.Default).Finally(() => _logger.Messages.Add("Start Pong"));

        private readonly Logger _logger;
    }
    internal class PingStopHandler : ApplicationStateHandler<StopApplicationEvent>, IBackgroundHandler
    {
        public PingStopHandler(Logger logger) => _logger = logger;

        protected override IObservable<Unit> Handle(StopApplicationEvent applicationStateEvent)
            => Observable.Return(Unit.Default).Finally(() => _logger.Messages.Add("Stop Ping"));

        private readonly Logger _logger;
    }

    internal class PongStopHandler : ApplicationStateHandler<StopApplicationEvent>, IBackgroundHandler
    {
        public PongStopHandler(Logger logger) => _logger = logger;

        protected override IObservable<Unit> Handle(StopApplicationEvent applicationStateEvent)
            => Observable.Return(Unit.Default).Finally(() => _logger.Messages.Add("Stop Pong"));

        private readonly Logger _logger;
    }
    internal class PingResumeHandler : ApplicationStateHandler<ResumeApplicationEvent>, IResumeHandler
    {
        public PingResumeHandler(Logger logger) => _logger = logger;

        protected override IObservable<Unit> Handle(ResumeApplicationEvent applicationStateEvent)
            => Observable.Return(Unit.Default).Finally(() => _logger.Messages.Add("Resume Ping"));

        private readonly Logger _logger;
    }

    internal class PongResumeHandler : ApplicationStateHandler<ResumeApplicationEvent>, IResumeHandler
    {
        public PongResumeHandler(Logger logger) => _logger = logger;

        protected override IObservable<Unit> Handle(ResumeApplicationEvent applicationStateEvent)
            => Observable.Return(Unit.Default).Finally(() => _logger.Messages.Add("Resume Pong"));

        private readonly Logger _logger;
    }
}