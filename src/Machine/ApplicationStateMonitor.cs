using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ApplicationState.Machine.Events;

namespace ApplicationState.Machine
{
    public class ApplicationStateMonitor : DisposableBase
    {
        public ApplicationStateMonitor(IApplicationState applicationState, ApplicationStatelessMachine statelessMachine)
        {
            applicationState
                .WhereHasSignal()
                .Select(x => new GainedSignalEvent(new Uri("//NavigationPage")))
                .Subscribe(connectivityChangedEvent => statelessMachine.Connect(connectivityChangedEvent))
                .DisposeWith(Garbage);

            applicationState
                .WhereHasNoSignal()
                .Select(x => new LostSignalEvent(new Uri("//NavigationPage")))
                .Subscribe(connectivityChangedEvent => statelessMachine.Disconnect(connectivityChangedEvent))
                .DisposeWith(Garbage);

            applicationState
               .OfType<InitializeApplicationEvent>()
               .Subscribe(applicationEvent => statelessMachine.Initialize(applicationEvent))
               .DisposeWith(Garbage);

            applicationState
               .OfType<StartApplicationEvent>()
               .Subscribe(applicationEvent => statelessMachine.Start(applicationEvent))
               .DisposeWith(Garbage);

            applicationState
               .OfType<StopApplicationEvent>()
               .Subscribe(applicationEvent => statelessMachine.Stop(applicationEvent))
               .DisposeWith(Garbage);

            StateChanged = statelessMachine.StateChanged;
        }

        public IObservable<ApplicationState> StateChanged { get; set; }
    }
}