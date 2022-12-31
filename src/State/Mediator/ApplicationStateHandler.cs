using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Unit = System.Reactive.Unit;

namespace State.Mediator
{
    public class ApplicationStateHandler<TEvent> : IApplicationStateHandler<TEvent>
        where TEvent : IStateEvent
    {
        /// <inheritdoc />
        IObservable<Unit> IApplicationStateHandler<TEvent>.Handle(TEvent stateEvent) => Handle(stateEvent);

        /// <inheritdoc />
        Task INotificationHandler<TEvent>.Handle(TEvent notification, CancellationToken cancellationToken) =>
            Handle(notification)
                .ToTask(cancellationToken);

        /// <summary>
        /// Handle a given <see cref="IStateEvent"/>.
        /// </summary>
        /// <param name="state">The application state event.</param>
        /// <returns>A completion notification.</returns>
        protected virtual IObservable<Unit> Handle(TEvent state) => Observable.Return(Unit.Default);
    }
}