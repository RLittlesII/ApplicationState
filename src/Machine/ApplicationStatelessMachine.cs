using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ApplicationState.Machine.Events;
using ApplicationState.Mediator;
using Stateless;

namespace ApplicationState.Machine
{
    public sealed class ApplicationStatelessMachine : StateMachine<ApplicationState, ApplicationEventTrigger>, IDisposable
    {
        public ApplicationStatelessMachine(IApplicationStateMediator applicationStateMediator,
            ApplicationState initialState = ApplicationState.Initial) :
            base(initialState) => ConfigureMachine(applicationStateMediator);

        public IObservable<ApplicationState> StateChanged { get; private set; } = Observable.Empty<ApplicationState>();

        public IObservable<string> UnhandledExceptions { get; set; } = Observable.Empty<string>();

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

        /// <inheritdoc />
        public void Dispose() => _garbage.Dispose();

        private void ConfigureMachine(IApplicationStateMediator applicationStateMediator)
        {
            var stateChange = new Subject<ApplicationState>().DisposeWith(_garbage);
            var unhandledExceptions = new Subject<string>().DisposeWith(_garbage);
            _start = SetTriggerParameters<ApplicationStateEvent>(ApplicationEventTrigger.Start);
            _stop = SetTriggerParameters<StopApplicationEvent>(ApplicationEventTrigger.Stop);
            _connect = SetTriggerParameters<GainedSignalEvent>(ApplicationEventTrigger.Connected);
            _disconnect = SetTriggerParameters<LostSignalEvent>(ApplicationEventTrigger.Disconnected);

            Configure(ApplicationState.Initial)
                .Permit(ApplicationEventTrigger.Connected, ApplicationState.Online)
                .Permit(ApplicationEventTrigger.Disconnected, ApplicationState.Offline)
                .Permit(ApplicationEventTrigger.Start, ApplicationState.Foreground)
                .Permit(ApplicationEventTrigger.Stop, ApplicationState.Background)
                .OnEntry(CommonEntry)
                .OnExit(CommonExit)
                .OnActivate(() => Fire(ApplicationEventTrigger.Start));

            Configure(ApplicationState.Background)
                .Permit(ApplicationEventTrigger.Start, ApplicationState.Foreground)
                .Permit(ApplicationEventTrigger.Deeplink, ApplicationState.Foreground)
                .Permit(ApplicationEventTrigger.Notification, ApplicationState.Foreground)
                .OnEntryFrom(_stop,  stateEvent => PublishStateEvent(stateEvent))
                .OnEntry(CommonEntry)
                .OnExit(CommonExit);

            Configure(ApplicationState.Foreground)
                .Ignore(ApplicationEventTrigger.Deeplink)
                .Ignore(ApplicationEventTrigger.Notification)
                .Permit(ApplicationEventTrigger.Stop, ApplicationState.Background)
                .Permit(ApplicationEventTrigger.Connected, ApplicationState.Online)
                .Permit(ApplicationEventTrigger.Disconnected, ApplicationState.Offline)
                .OnEntryFrom(_start,  stateEvent => PublishStateEvent(stateEvent))
                .OnEntryFrom(_stop,  stateEvent => PublishStateEvent(stateEvent))
                .OnEntry(CommonEntry)
                .OnExit(CommonExit);

            Configure(ApplicationState.Online)
                .SubstateOf(ApplicationState.Foreground)
                .SubstateOf(ApplicationState.Background)
                .Permit(ApplicationEventTrigger.Stop, ApplicationState.Background)
                .Permit(ApplicationEventTrigger.Disconnected, ApplicationState.Offline)
                .OnEntryFrom(_connect,  connectivityChangedEvent => PublishStateEvent(connectivityChangedEvent))
                .OnEntry(CommonEntry)
                .OnExit(CommonExit);

            Configure(ApplicationState.Offline)
                .SubstateOf(ApplicationState.Foreground)
                .SubstateOf(ApplicationState.Background)
                .Ignore(ApplicationEventTrigger.Deeplink)
                .Ignore(ApplicationEventTrigger.Notification)
                .Permit(ApplicationEventTrigger.Start, ApplicationState.Foreground)
                .Permit(ApplicationEventTrigger.Connected, ApplicationState.Online)
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

            void PublishStateEvent(ApplicationStateEvent stateEvent)
            {
                using var _ = applicationStateMediator.Notify(stateEvent).Subscribe();
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