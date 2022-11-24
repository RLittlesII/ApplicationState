using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ApplicationState.Machine.Events
{
    public class ApplicationEvents : DisposableBase, IApplicationEvents
    {
        public ApplicationEvents(IApplicationTriggers applicationTriggers)
        {
            var current = new Uri("//NavigationPage");

            _applicationStateEvents = new Subject<ApplicationStateEvent>().DisposeWith(Garbage);

            applicationTriggers
                .Initializing
                .Select(_ => new InitializeApplicationEvent(current))
                .Subscribe(_applicationStateEvents)
                .DisposeWith(Garbage);

            applicationTriggers
                .Starting
                .Select(_ => new StartApplicationEvent(current))
                .Subscribe(_applicationStateEvents)
                .DisposeWith(Garbage);

            applicationTriggers
                .Pausing
                .Select(_ => new StopApplicationEvent(current))
                .Subscribe(_applicationStateEvents)
                .DisposeWith(Garbage);

            applicationTriggers
                .Resuming
                .Select(_ => new ResumeApplicationEvent(current))
                .Subscribe(_applicationStateEvents)
                .DisposeWith(Garbage);
        }

        public IDisposable Subscribe(IObserver<ApplicationStateEvent> observer) => _applicationStateEvents.Subscribe(observer);

        private readonly Subject<ApplicationStateEvent> _applicationStateEvents;
    }
}