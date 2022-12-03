using System;
using System.Reactive.Linq;
using ApplicationState.Machine.Application;
using ApplicationState.Machine.Network.Offline;
using ApplicationState.Machine.Network.Online;
using ApplicationState.Mediator;

namespace ApplicationState.Machine.Network;

public sealed class NetworkStateMachine : ObservableStateMachine<NetworkMachineState, NetworkMachineTrigger>
{
    public NetworkStateMachine(IApplicationStateMediator mediator,
        NetworkMachineState initialState) :
        base(initialState) => ConfigureMachine(mediator);

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

    private void ConfigureMachine(IApplicationStateMediator applicationStateMediator)
    {
        _connect = SetTriggerParameters<GainedSignalEvent>(NetworkMachineTrigger.Connected);
        _disconnect = SetTriggerParameters<LostSignalEvent>(NetworkMachineTrigger.Disconnected);

        Configure(NetworkMachineState.Initial)
            .Permit(NetworkMachineTrigger.Connected, NetworkMachineState.Online)
            .Permit(NetworkMachineTrigger.Disconnected, NetworkMachineState.Offline)
            .OnEntry(CommonEntry)
            .OnExit(CommonExit)
            .OnActivate(() => Fire(NetworkMachineTrigger.Connected));

        Configure(NetworkMachineState.Online)
            .Permit(NetworkMachineTrigger.Disconnected, NetworkMachineState.Offline)
            .OnEntryFrom(_connect, connectivityChangedEvent => PublishStateEvent(connectivityChangedEvent))
            .OnEntry(CommonEntry)
            .OnExit(CommonExit);

        Configure(NetworkMachineState.Offline)
            .Permit(NetworkMachineTrigger.Connected, NetworkMachineState.Online)
            .OnEntryFrom(_disconnect, connectivityChangedEvent => PublishStateEvent(connectivityChangedEvent))
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

    private TriggerWithParameters<GainedSignalEvent> _connect;
    private TriggerWithParameters<LostSignalEvent> _disconnect;
}