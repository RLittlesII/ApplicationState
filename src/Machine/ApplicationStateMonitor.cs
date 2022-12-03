using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ApplicationState.Machine.Application;
using ApplicationState.Machine.Application.Background;
using ApplicationState.Machine.Application.Foreground;
using ApplicationState.Machine.Application.Initialize;
using ApplicationState.Machine.Network;
using ApplicationState.Machine.Network.Offline;
using ApplicationState.Machine.Network.Online;

namespace ApplicationState.Machine
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
                    (appState, networkState) => StateFactory(appState, networkState));

            ApplicationState StateFactory(ApplicationMachineState appState, NetworkMachineState networkState) =>
                new ApplicationState(appState == ApplicationMachineState.Foreground, networkState == NetworkMachineState.Online);
        }

        public IObservable<ApplicationState> State { get; set; }
    }

    public record ApplicationState(bool Foreground, bool Connected);
}