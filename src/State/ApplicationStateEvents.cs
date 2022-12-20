using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ApplicationState.Application;
using ApplicationState.Application.Background;
using ApplicationState.Application.Foreground;
using ApplicationState.Application.Initialize;
using ApplicationState.Network;
using ApplicationState.Network.Offline;
using ApplicationState.Network.Online;

namespace ApplicationState
{
    public class ApplicationStateEvents : DisposableBase, IApplicationStateEvents
    {
        // NOTE: [rlittlesii: November 26, 2022] encapsulate ay other application, operating system or device notification here.
        public ApplicationStateEvents(IApplicationLifecycleState applicationLifecycleState, INetworkState networkState)
        {
            _applicationStateEvents = new Subject<ApplicationStateEvent>().DisposeWith(Garbage);

            applicationLifecycleState
                .Initializing()
                .Select(_ => new InitializeApplicationEvent())
                .Subscribe(_applicationStateEvents)
                .DisposeWith(Garbage);

            applicationLifecycleState
                .Starting()
                .Select(_ => new StartApplicationEvent())
                .Subscribe(_applicationStateEvents)
                .DisposeWith(Garbage);

            applicationLifecycleState
                .Pausing()
                .Select(_ => new StopApplicationEvent())
                .Subscribe(_applicationStateEvents)
                .DisposeWith(Garbage);

            applicationLifecycleState
                .Resuming()
                .Select(_ => new ResumeApplicationEvent())
                .Subscribe(_applicationStateEvents)
                .DisposeWith(Garbage);

            networkState
                .WhereHasSignal()
                .Select(_ => new GainedSignalEvent())
                .Subscribe(_applicationStateEvents)
                .DisposeWith(Garbage);

            networkState
                .WhereHasNoSignal()
                .Select(_ => new LostSignalEvent())
                .Subscribe(_applicationStateEvents)
                .DisposeWith(Garbage);
        }

        public IDisposable Subscribe(IObserver<ApplicationStateEvent> observer) => _applicationStateEvents.Subscribe(observer);

        private readonly Subject<ApplicationStateEvent> _applicationStateEvents;
    }
}