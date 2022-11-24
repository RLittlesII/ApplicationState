using System;
using System.Reactive;

namespace ApplicationState.Machine
{
    public interface IApplicationTriggers
    {
        IObservable<Unit> Initializing { get; }
        IObservable<Unit> Starting { get; }
        IObservable<Unit> Pausing { get; }
        IObservable<Unit> Resuming { get; }
    }
}