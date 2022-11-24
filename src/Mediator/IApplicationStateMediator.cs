using System;
using System.Reactive;

namespace ApplicationState.Mediator
{
    public interface IApplicationStateMediator
    {
        IObservable<Unit> Notify<TEvent>(TEvent notification)
            where TEvent : IStateEvent;
    }
}