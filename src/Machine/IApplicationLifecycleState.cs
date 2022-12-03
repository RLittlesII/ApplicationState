using System;
using System.Reactive.Linq;

namespace ApplicationState.Machine
{
    public interface IApplicationLifecycleState : IObservable<LifecycleState>
    {
        IObservable<LifecycleState> Initializing() => this.Where(x => x is LifecycleState.Initializing);
        IObservable<LifecycleState> Pausing() => this.Where(x => x is LifecycleState.Pausing);
        IObservable<LifecycleState> Starting() => this.Where(x => x is LifecycleState.Starting);
        IObservable<LifecycleState> Resuming() => this.Where(x => x is LifecycleState.Resuming);
    }

    public enum LifecycleState
    {
        Initializing,
        Starting,
        Pausing,
        Resuming
    }
}