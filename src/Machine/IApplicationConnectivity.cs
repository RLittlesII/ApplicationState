using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ApplicationState.Machine.Events;

namespace ApplicationState.Machine
{
    /// <summary>
    /// Interface representing the Network Connectivity features.
    /// </summary>
    public interface IApplicationConnectivity : IObservable<ConnectivityChangedEvent>
    {
        /// <summary>
        /// Gets the network access.
        /// </summary>
        NetworkAccess NetworkAccess { get; }

        /// <summary>
        /// Gets the connection profiles.
        /// </summary>
        IReadOnlyList<ConnectionProfile> Profiles { get; }

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
        /// Gets an observable sequence notifying if the device has connectivity.
        /// </summary>
        /// <returns></returns>
        IObservable<ConnectivityChangedEvent> WhereHasSignal() =>
            this
                .DistinctUntilChanged(changedEvent => changedEvent.NetworkAccess)
                .Where(changedEvent => changedEvent.HasSignal())
                .DistinctUntilChanged()
                .Publish()
                .RefCount();

        /// <summary>
        /// Gets an observable sequence notifying if the device has a signal.
        /// </summary>
        /// <returns></returns>
        IObservable<ConnectivityChangedEvent> WhereHasNoSignal() =>
            this
                .DistinctUntilChanged(changedEvent => changedEvent.NetworkAccess)
                .Where(changedEvent => !changedEvent.HasSignal())
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