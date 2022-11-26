namespace ApplicationState.Machine.Tests.State
{
    public class StatelessWrapper<TMachine, TState, TTrigger>
        where TMachine : StateMachine<TState, TTrigger>
    {
        public StatelessWrapper(StateMachine<TState, TTrigger> machine)
        {
            var thing = new Subject<StateMachine<TState, TTrigger>.Transition>();

            machine.OnTransitionCompleted(transition => thing.OnNext(transition));

            CurrentState = thing.AsObservable();
        }

        public IObservable<StateMachine<TState, TTrigger>.Transition> CurrentState { get; }
    }
}