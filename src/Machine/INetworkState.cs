using System;
using System.Reactive.Linq;
using ApplicationState.Machine.Events;

namespace ApplicationState.Machine
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
                .DistinctUntilChanged(changedEvent => changedEvent.NetworkAccess)
                .Where(changedEvent => !changedEvent.HasSignal())
                .DistinctUntilChanged()
                .Publish()
                .RefCount();

        /// <summary>
        /// Gets an observable sequence notifying if the device has connectivity.
        /// </summary>
        /// <returns></returns>
        IObservable<bool> HasSignal() =>
            this
                .DistinctUntilChanged(changedEvent => changedEvent.NetworkAccess)
                .Select(changedEvent => changedEvent.HasSignal())
                .DistinctUntilChanged()
                .Publish()
                .RefCount();

        /// <summary>
        /// Gets an observable sequence notifying that signal has degraded.
        /// </summary>
        /// <returns></returns>
        IObservable<bool> SignalDegraded() =>
            this
                .DistinctUntilChanged(changedEvent => changedEvent.NetworkAccess)
                .Select(changedEvent => changedEvent.Degraded())
                .DistinctUntilChanged()
                .Publish()
                .RefCount();
    }
}