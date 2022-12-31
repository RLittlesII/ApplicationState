using System;
using System.Reactive.Subjects;
using State.Application;

namespace State.Tests
{
    internal class ApplicationStateEventsMock : IApplicationStateEvents
    {
        public void Notify(ApplicationStateEvent stateEvent) => _applicationStateEvents.OnNext(stateEvent);
        public void Notify(params ApplicationStateEvent[] stateEvent)
        {
            foreach (var applicationStateEvent in stateEvent)
            {
                Notify(applicationStateEvent);
            }
        }

        public IDisposable Subscribe(IObserver<ApplicationStateEvent> observer) => _applicationStateEvents.Subscribe(observer);

        private readonly Subject<ApplicationStateEvent> _applicationStateEvents = new Subject<ApplicationStateEvent>();
    }
}