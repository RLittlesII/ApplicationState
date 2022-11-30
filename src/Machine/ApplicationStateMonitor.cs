using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ApplicationState.Machine.Background;
using ApplicationState.Machine.Foreground;
using ApplicationState.Machine.Initialize;
using ApplicationState.Machine.Offline;
using ApplicationState.Machine.Online;

namespace ApplicationState.Machine
{
    public class ApplicationStateMonitor : DisposableBase
    {
        public ApplicationStateMonitor(IApplicationState applicationState, ApplicationStatelessMachine statelessMachine)
        {
            applicationState
                .OfType<GainedSignalEvent>()
                .Subscribe(gainedSignal => statelessMachine.Connect(gainedSignal))
                .DisposeWith(Garbage);

            applicationState
                .OfType<LostSignalEvent>()
                .Subscribe(lostSignal => statelessMachine.Disconnect(lostSignal))
                .DisposeWith(Garbage);

            applicationState
               .OfType<InitializeApplicationEvent>()
               .Subscribe(initialize => statelessMachine.Initialize(initialize))
               .DisposeWith(Garbage);

            applicationState
               .OfType<StartApplicationEvent>()
               .Subscribe(startApplication => statelessMachine.Start(startApplication))
               .DisposeWith(Garbage);

            applicationState
               .OfType<StopApplicationEvent>()
               .Subscribe(stopApplication => statelessMachine.Stop(stopApplication))
               .DisposeWith(Garbage);

            StateChanged = statelessMachine.StateChanged;
        }

        public IObservable<ApplicationMachineState> StateChanged { get; set; }
    }
}