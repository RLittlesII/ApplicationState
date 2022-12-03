using System;
using System.Reactive.Linq;

namespace ApplicationState.Machine.Network
{
    /// <summary>
    /// Interface representing the Network Connectivity.
    /// </summary>
    public interface INetworkState : IObservable<NetworkStateChangedEvent>
    {
        /// <summary>
        /// Gets an observable sequence notifying if the device has connectivity.
        /// </summary>
        /// <returns>The sequence of events with signal.</returns>
        IObservable<NetworkStateChangedEvent> WhereHasSignal() =>
            this
                .OfType<NetworkStateChangedEvent>()
                .DistinctUntilChanged(changedEvent => changedEvent.NetworkAccess)
                .Where(changedEvent => changedEvent.HasSignal())
                .DistinctUntilChanged()
                .Publish()
                .RefCount();

        /// <summary>
        /// Gets an observable sequence notifying if the device has a signal.
        /// </summary>
        /// <returns>The sequence of events with no signal.</returns>
        IObservable<NetworkStateChangedEvent> WhereHasNoSignal() =>
            this
                .OfType<NetworkStateChangedEvent>()
                .DistinctUntilChanged(changedEvent => changedEvent.NetworkAccess)
                .Where(changedEvent => !changedEvent.HasSignal())
                .DistinctUntilChanged()
                .Publish()
                .RefCount();
    }
}