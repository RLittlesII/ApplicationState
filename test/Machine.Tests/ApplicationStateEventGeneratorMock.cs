using System;
using System.Reactive.Subjects;
using ApplicationState.Machine.Application;

namespace ApplicationState.Machine.Tests
{
    internal class ApplicationStateEventGeneratorMock : IApplicationStateEventGenerator
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