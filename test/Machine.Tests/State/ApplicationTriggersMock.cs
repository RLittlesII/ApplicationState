namespace ApplicationState.Machine.Tests.State
{
    internal class ApplicationTriggersMock : IApplicationTriggers
    {
        public IObservable<Unit> Initializing => _initializing.AsObservable();
        public IObservable<Unit> Starting => _starting.AsObservable();
        public IObservable<Unit> Pausing => _pausing.AsObservable();
        public IObservable<Unit> Resuming => _resuming.AsObservable();

        public void Initialize() => _initializing.OnNext(Unit.Default);
        public void Start() => _initializing.OnNext(Unit.Default);
        public void Pause() => _initializing.OnNext(Unit.Default);
        public void Resume() => _initializing.OnNext(Unit.Default);

        private readonly Subject<Unit> _initializing = new Subject<Unit>();
        private readonly Subject<Unit> _starting = new Subject<Unit>();
        private readonly Subject<Unit> _pausing = new Subject<Unit>();
        private readonly Subject<Unit> _resuming = new Subject<Unit>();
    }
}