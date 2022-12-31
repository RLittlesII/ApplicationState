using System;
using System.Reactive.Linq;

namespace State
{
    public interface IApplicationLifecycleState : IObservable<LifecycleState>
    {
        IObservable<LifecycleState> Initializing() =>
            this.Where(state => state is LifecycleState.Initializing);
        IObservable<LifecycleState> Pausing() =>
            this.Where(state => state is LifecycleState.Pausing);
        IObservable<LifecycleState> Starting() =>
            this.Where(state => state is LifecycleState.Starting);
        IObservable<LifecycleState> Resuming() =>
            this.Where(state => state is LifecycleState.Resuming);
    }

    public enum LifecycleState
    {
        Initializing,
        Starting,
        Pausing,
        Resuming
    }
}