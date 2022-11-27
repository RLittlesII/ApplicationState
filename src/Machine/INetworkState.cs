using System;
using ApplicationState.Machine.Events;

namespace ApplicationState.Machine
{
    /// <summary>
    /// Interface representing the Network Connectivity.
    /// </summary>
    public interface INetworkState : IObservable<NetworkStateChangedEvent>
    {
    }
}