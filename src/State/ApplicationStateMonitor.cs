using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ApplicationState.Application;
using ApplicationState.Application.Background;
using ApplicationState.Application.Foreground;
using ApplicationState.Application.Initialize;
using ApplicationState.Network;
using ApplicationState.Network.Offline;
using ApplicationState.Network.Online;
using ReactiveUI;

namespace ApplicationState
{
    public class ApplicationStateMonitor : DisposableBase
    {
        public ApplicationStateMonitor(IApplicationStateEvents applicationStateEvents,
            ApplicationStateMachine applicationStateMachine,
            NetworkStateMachine networkStateMachine)
        {
            applicationStateEvents
                .OfType<GainedSignalEvent>()
                .Subscribe(gainedSignal => networkStateMachine.Connect(gainedSignal))
                .DisposeWith(Garbage);

            applicationStateEvents
                .OfType<LostSignalEvent>()
                .Subscribe(lostSignal => networkStateMachine.Disconnect(lostSignal))
                .DisposeWith(Garbage);

            applicationStateEvents
               .OfType<InitializeApplicationEvent>()
               .Subscribe(initialize => applicationStateMachine.Initialize(initialize))
               .DisposeWith(Garbage);

            applicationStateEvents
               .OfType<StartApplicationEvent>()
               .Subscribe(startApplication => applicationStateMachine.Start(startApplication))
               .DisposeWith(Garbage);

            applicationStateEvents
               .OfType<StopApplicationEvent>()
               .Subscribe(stopApplication => applicationStateMachine.Stop(stopApplication))
               .DisposeWith(Garbage);

            State = applicationStateMachine
                .StateChanged
                .CombineLatest(networkStateMachine.StateChanged,
                    (appState, networkState) => StateFactory(appState, networkState))
                .StartWith(ApplicationState.Default);

            static ApplicationState StateFactory(ApplicationMachineState appState, NetworkMachineState networkState) =>
                new(appState == ApplicationMachineState.Foreground, networkState == NetworkMachineState.Online);
        }

        public IObservable<ApplicationState> State { get; set; }
    }

    public record ApplicationState(bool Foreground, bool Connected) : ReactiveRecord
    {
        public static ApplicationState Default { get; } = new(false, false);
    }
}