using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ApplicationState.Machine.Background;
using ApplicationState.Machine.Foreground;
using ApplicationState.Machine.Initialize;
using ApplicationState.Machine.Offline;
using ApplicationState.Machine.Online;
using ApplicationState.Mediator;
using Stateless;

namespace ApplicationState.Machine
{
    public sealed class ApplicationStatelessMachine : StateMachine<ApplicationMachineState, ApplicationMachineTrigger>, IDisposable
    {
        public ApplicationStatelessMachine(IApplicationStateMediator applicationStateMediator,
            ApplicationMachineState initialState = ApplicationMachineState.Initial) :
            base(initialState) => ConfigureMachine(applicationStateMediator);

        public IObservable<ApplicationMachineState> StateChanged { get; private set; } = Observable.Empty<ApplicationMachineState>();

        public IObservable<string> UnhandledExceptions { get; private set; } = Observable.Empty<string>();

        /// <summary>
        /// Connect the application.
        /// </summary>
        /// <param name="event">The connect event.</param>
        public void Connect(GainedSignalEvent @event) =>
            Fire(_connect, @event);

        /// <summary>
        /// Disconnect the application.
        /// </summary>
        /// <param name="event">The disconnect event.</param>
        public void Disconnect(LostSignalEvent @event) =>
            Fire(_disconnect, @event);

        /// <summary>
        /// Initialize the application.
        /// </summary>
        /// <param name="event">The initialization event.</param>
        public void Initialize(InitializeApplicationEvent @event) =>
            Fire(_start, @event);

        /// <summary>
        /// Resume the application.
        /// </summary>
        /// <param name="event">The resume event.</param>
        public void Resume(ResumeApplicationEvent @event) =>
            Fire(_start, @event);

        /// <summary>
        /// Start the application.
        /// </summary>
        /// <param name="event">The start event.</param>
        public void Start(StartApplicationEvent @event) =>
            Fire(_start, @event);

        /// <summary>
        /// Stop the application.
        /// </summary>
        /// <param name="event">The stop event.</param>
        public void Stop(StopApplicationEvent @event) =>
            Fire(_stop, @event);

        /// <inheritdoc />
        public void Dispose() => _garbage.Dispose();

        private void ConfigureMachine(IApplicationStateMediator applicationStateMediator)
        {
            var stateChange = new Subject<ApplicationMachineState>().DisposeWith(_garbage);
            var unhandledExceptions = new Subject<string>().DisposeWith(_garbage);

            _start = SetTriggerParameters<ApplicationStateEvent>(ApplicationMachineTrigger.Start);
            _stop = SetTriggerParameters<StopApplicationEvent>(ApplicationMachineTrigger.Stop);
            _connect = SetTriggerParameters<GainedSignalEvent>(ApplicationMachineTrigger.Connected);
            _disconnect = SetTriggerParameters<LostSignalEvent>(ApplicationMachineTrigger.Disconnected);

            Configure(ApplicationMachineState.Initial)
                .Permit(ApplicationMachineTrigger.Connected, ApplicationMachineState.Online)
                .Permit(ApplicationMachineTrigger.Disconnected, ApplicationMachineState.Offline)
                .Permit(ApplicationMachineTrigger.Start, ApplicationMachineState.Foreground)
                .Permit(ApplicationMachineTrigger.Stop, ApplicationMachineState.Background)
                .OnEntry(CommonEntry)
                .OnExit(CommonExit)
                .OnActivate(() => Fire(ApplicationMachineTrigger.Start));

            Configure(ApplicationMachineState.Background)
                .Permit(ApplicationMachineTrigger.Start, ApplicationMachineState.Foreground)
                .Permit(ApplicationMachineTrigger.Deeplink, ApplicationMachineState.Foreground)
                .Permit(ApplicationMachineTrigger.Notification, ApplicationMachineState.Foreground)
                .OnEntryFrom(_stop,  stateEvent => PublishStateEvent(stateEvent))
                .OnEntry(CommonEntry)
                .OnExit(CommonExit);

            Configure(ApplicationMachineState.Foreground)
                .Ignore(ApplicationMachineTrigger.Deeplink)
                .Ignore(ApplicationMachineTrigger.Notification)
                .Permit(ApplicationMachineTrigger.Stop, ApplicationMachineState.Background)
                .Permit(ApplicationMachineTrigger.Connected, ApplicationMachineState.Online)
                .Permit(ApplicationMachineTrigger.Disconnected, ApplicationMachineState.Offline)
                .OnEntryFrom(_start,  stateEvent => PublishStateEvent(stateEvent))
                .OnEntryFrom(_stop,  stateEvent => PublishStateEvent(stateEvent))
                .OnEntry(CommonEntry)
                .OnExit(CommonExit);

            Configure(ApplicationMachineState.Online)
                .SubstateOf(ApplicationMachineState.Foreground)
                .SubstateOf(ApplicationMachineState.Background)
                .Permit(ApplicationMachineTrigger.Stop, ApplicationMachineState.Background)
                .Permit(ApplicationMachineTrigger.Disconnected, ApplicationMachineState.Offline)
                .OnEntryFrom(_connect,  connectivityChangedEvent => PublishStateEvent(connectivityChangedEvent))
                .OnEntry(CommonEntry)
                .OnExit(CommonExit);

            Configure(ApplicationMachineState.Offline)
                .SubstateOf(ApplicationMachineState.Foreground)
                .SubstateOf(ApplicationMachineState.Background)
                .Ignore(ApplicationMachineTrigger.Deeplink)
                .Ignore(ApplicationMachineTrigger.Notification)
                .Permit(ApplicationMachineTrigger.Start, ApplicationMachineState.Foreground)
                .Permit(ApplicationMachineTrigger.Connected, ApplicationMachineState.Online)
                .OnEntryFrom(_disconnect,  connectivityChangedEvent => PublishStateEvent(connectivityChangedEvent))
                .OnEntry(CommonEntry)
                .OnExit(CommonExit);

            OnUnhandledTrigger((state, trigger) =>
                unhandledExceptions.OnNext($"{trigger} is not configured for {state}"));

            OnTransitionCompleted(transition => stateChange.OnNext(transition.Destination));

            StateChanged =
                stateChange
                    .AsObservable()
                    .Publish()
                    .RefCount();

            UnhandledExceptions =
                unhandledExceptions
                    .AsObservable()
                    .Publish()
                    .RefCount();

            void CommonEntry(Transition transition) =>
                TraceTransition(transition);

            void CommonExit(Transition transition) =>
                TraceTransition(transition);

            void PublishStateEvent(ApplicationStateEvent? stateEvent)
            {
                if (stateEvent != null)
                {
                    using var _ =
                        applicationStateMediator
                            .Notify(stateEvent)
                            .Finally(() => Console.WriteLine(stateEvent.ToString()))
                            .Subscribe();
                }
            }

            void TraceTransition(Transition _) => Console.WriteLine($"{_}");
        }

        private TriggerWithParameters<GainedSignalEvent> _connect;
        private TriggerWithParameters<LostSignalEvent> _disconnect;
        private TriggerWithParameters<ApplicationStateEvent> _start;
        private TriggerWithParameters<StopApplicationEvent> _stop;
        private readonly CompositeDisposable _garbage = new CompositeDisposable();
    }
}