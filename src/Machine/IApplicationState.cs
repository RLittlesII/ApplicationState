using System;
using System.Reactive.Linq;
using ApplicationState.Machine.Events;

namespace ApplicationState.Machine
{
    /// <summary>
    /// Interface representing <see cref="ApplicationStateEvent"/> stream.
    /// </summary>
    public interface IApplicationState : IObservable<ApplicationStateEvent>
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