using System;
using System.Reactive;

namespace State.Mediator
{
    public interface IApplicationStateMediator
    {
        IObservable<Unit> Notify<TEvent>(TEvent notification)
            where TEvent : IStateEvent;
    }
}