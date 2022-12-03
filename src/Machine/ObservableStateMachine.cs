using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Stateless;

namespace ApplicationState.Machine;

public abstract class ObservableStateMachine<TState, TTrigger> : StateMachine<TState, TTrigger>, IDisposable
{
    protected ObservableStateMachine(TState initialState) : base(initialState)
    {
        var stateChange = new Subject<TState>().DisposeWith(Garbage);
        var unhandledExceptions = new Subject<string>().DisposeWith(Garbage);

        OnUnhandledTrigger((state, trigger) =>
            unhandledExceptions.OnNext($"{trigger} is not configured for {state}"));

        OnTransitionCompleted(transition => stateChange.OnNext(transition.Destination));

        StateChanged =
            stateChange
                .AsObservable()
                .Publish()
                .RefCount()
                .Do(_ => {});

        UnhandledExceptions =
            unhandledExceptions
                .AsObservable()
                .Publish()
                .RefCount()
                .Do(_ => {});
    }

    public IObservable<TState> StateChanged { get; }

    public IObservable<string> UnhandledExceptions { get; }

    protected CompositeDisposable Garbage { get; } = new CompositeDisposable();

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
        }
    }

    protected void CommonEntry(Transition transition) =>
        TraceTransition(transition);

    protected void CommonExit(Transition transition) =>
        TraceTransition(transition);

    protected void TraceTransition(Transition _) => Console.WriteLine($"{_}");

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}