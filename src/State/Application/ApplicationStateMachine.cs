using System;
using System.Reactive.Linq;
using ApplicationState.Application.Background;
using ApplicationState.Application.Foreground;
using ApplicationState.Application.Initialize;
using ApplicationState.Mediator;

namespace ApplicationState.Application
{
    public sealed class ApplicationStateMachine : ObservableStateMachine<ApplicationMachineState, ApplicationMachineTrigger>
    {
        public ApplicationStateMachine(IApplicationStateMediator applicationStateMediator,
            ApplicationMachineState initialState = ApplicationMachineState.Initial) :
            base(initialState) => ConfigureMachine(applicationStateMediator);

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

        private void ConfigureMachine(IApplicationStateMediator applicationStateMediator)
        {
            _start = SetTriggerParameters<ApplicationStateEvent>(ApplicationMachineTrigger.Start);
            _stop = SetTriggerParameters<StopApplicationEvent>(ApplicationMachineTrigger.Stop);

            Configure(ApplicationMachineState.Initial)
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
                .OnEntryFrom(_start,  stateEvent => PublishStateEvent(stateEvent))
                .OnEntryFrom(_stop,  stateEvent => PublishStateEvent(stateEvent))
                .OnEntry(CommonEntry)
                .OnExit(CommonExit);

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
        }

        private TriggerWithParameters<ApplicationStateEvent> _start;
        private TriggerWithParameters<StopApplicationEvent> _stop;
    }
}