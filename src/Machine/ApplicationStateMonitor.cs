using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ApplicationState.Machine.Events;

namespace ApplicationState.Machine
{
    public class ApplicationStateMonitor : DisposableBase
    {
        public ApplicationStateMonitor(
            IApplicationEvents applicationEvents,
            INetworkState networkState,
            ApplicationStatelessMachine statelessMachine)
        {
            applicationEvents
               .OfType<InitializeApplicationEvent>()
               .Subscribe(applicationEvent => statelessMachine.Initialize(applicationEvent))
               .DisposeWith(Garbage);

            applicationEvents
               .OfType<StartApplicationEvent>()
               .Subscribe(applicationEvent => statelessMachine.Start(applicationEvent))
               .DisposeWith(Garbage);

            applicationEvents
               .OfType<StopApplicationEvent>()
               .Subscribe(applicationEvent => statelessMachine.Stop(applicationEvent))
               .DisposeWith(Garbage);

            networkState
               .WhereHasSignal()
               .Select(x => new GainedSignalEvent(new Uri("//NavigationPage")))
               .Subscribe(connectivityChangedEvent => statelessMachine.Connect(connectivityChangedEvent))
               .DisposeWith(Garbage);

            networkState
               .WhereHasNoSignal()
               .Select(x => new LostSignalEvent(new Uri("//NavigationPage")))
               .Subscribe(connectivityChangedEvent => statelessMachine.Disconnect(connectivityChangedEvent))
               .DisposeWith(Garbage);

            StateChanged = statelessMachine.StateChanged;
        }

        public IObservable<ApplicationState> StateChanged { get; set; }

        // Foreground
        // Initialize the things
        // Connect the things
        // Background
        // Disconnect the things
        // Figure out how to make this true from a background launch.
        // Persist state
        // Online
        // Send cancellation token?
        // Play operation queue
        // Offline
        // Send cancellation token?
        // Queue operations
    }
}