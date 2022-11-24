using System;
using System.Reactive.Subjects;
using ApplicationState.Machine.Events;

namespace ApplicationState.Machine.Tests.State
{
    internal class ApplicationEventsMock : IApplicationEvents
    {
        public void Notify(ApplicationStateEvent stateEvent) => _applicationStateEvents.OnNext(stateEvent);

        public IDisposable Subscribe(IObserver<ApplicationStateEvent> observer) => _applicationStateEvents.Subscribe(observer);

        private Subject<ApplicationStateEvent> _applicationStateEvents = new Subject<ApplicationStateEvent>();
    }
}