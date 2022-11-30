using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ApplicationState.Machine.Background;
using ApplicationState.Machine.Foreground;
using ApplicationState.Machine.Initialize;
using ApplicationState.Machine.Offline;
using ApplicationState.Machine.Online;

namespace ApplicationState.Machine
{
    public class ApplicationState : DisposableBase, IApplicationState
    {
        // NOTE: [rlittlesii: November 26, 2022] encapsulate ay other application, operating system or device notification here.
        public ApplicationState(IApplicationEvents applicationEvents, INetworkState networkState)
        {
            _applicationStateEvents = new Subject<ApplicationStateEvent>().DisposeWith(Garbage);

            applicationEvents
                .Initializing
                .Select(_ => new InitializeApplicationEvent())
                .Subscribe(_applicationStateEvents)
                .DisposeWith(Garbage);

            applicationEvents
                .Starting
                .Select(_ => new StartApplicationEvent())
                .Subscribe(_applicationStateEvents)
                .DisposeWith(Garbage);

            applicationEvents
                .Pausing
                .Select(_ => new StopApplicationEvent())
                .Subscribe(_applicationStateEvents)
                .DisposeWith(Garbage);

            applicationEvents
                .Resuming
                .Select(_ => new ResumeApplicationEvent())
                .Subscribe(_applicationStateEvents)
                .DisposeWith(Garbage);

            networkState
                .WhereHasSignal()
                .Select(networkStateChangedEvent => new GainedSignalEvent())
                .Subscribe(_applicationStateEvents)
                .DisposeWith(Garbage);

            networkState
                .WhereHasNoSignal()
                .Select(networkStateChangedEvent => new LostSignalEvent())
                .Subscribe(_applicationStateEvents)
                .DisposeWith(Garbage);
        }

        public IDisposable Subscribe(IObserver<ApplicationStateEvent> observer) => _applicationStateEvents.Subscribe(observer);

        private readonly Subject<ApplicationStateEvent> _applicationStateEvents;
    }
}