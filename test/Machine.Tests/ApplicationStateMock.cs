using System;
using System.Reactive.Subjects;

namespace ApplicationState.Machine.Tests
{
    internal class ApplicationStateMock : IApplicationState
    {
        public void Notify(ApplicationStateEvent stateEvent) => _applicationStateEvents.OnNext(stateEvent);

        public IDisposable Subscribe(IObserver<ApplicationStateEvent> observer) => _applicationStateEvents.Subscribe(observer);

        private readonly Subject<ApplicationStateEvent> _applicationStateEvents = new Subject<ApplicationStateEvent>();
    }
}