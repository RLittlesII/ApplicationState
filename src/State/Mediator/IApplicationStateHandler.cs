using System;
using MediatR;
using Unit = System.Reactive.Unit;

namespace State.Mediator
{
    public interface IApplicationStateHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : IStateEvent
    {
        /// <summary>
        /// Handle a given <see cref="IStateEvent"/>.
        /// </summary>
        /// <param name="stateEvent">The application state event.</param>
        /// <returns>A completion notification.</returns>
        IObservable<Unit> Handle(TEvent stateEvent);
    }
}