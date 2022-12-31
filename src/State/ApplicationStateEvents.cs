using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using State.Application;
using State.Application.Background;
using State.Application.Foreground;
using State.Application.Initialize;
using State.Network;
using State.Network.Offline;
using State.Network.Online;

namespace State
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