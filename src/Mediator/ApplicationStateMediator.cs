using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ApplicationState.Mediator
{
    public class ApplicationStateMediator : IApplicationStateMediator, IMediator
    {
        public ApplicationStateMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <inheritdoc />
        Task<TResponse> ISender.Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken) =>
            _mediator.Send(request, cancellationToken);

        /// <inheritdoc />
        Task<object?> ISender.Send(object request, CancellationToken cancellationToken) =>
            _mediator.Send(request, cancellationToken);

        /// <inheritdoc />
        IAsyncEnumerable<TResponse> ISender.CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken) =>
            _mediator.CreateStream(request, cancellationToken);

        /// <inheritdoc />
        IAsyncEnumerable<object?> ISender.CreateStream(object request, CancellationToken cancellationToken) =>
            _mediator.CreateStream(request, cancellationToken);

        /// <inheritdoc />
        Task IPublisher.Publish(object notification, CancellationToken cancellationToken) =>
            _mediator.Publish(notification, cancellationToken);

        /// <inheritdoc />
        Task IPublisher.Publish<TNotification>(TNotification notification, CancellationToken cancellationToken) =>
            _mediator.Publish(notification, cancellationToken);

        /// <inheritdoc />
        IObservable<System.Reactive.Unit> IApplicationStateMediator.Notify<TNotification>(TNotification notification) =>
            Observable.Create<System.Reactive.Unit>(observer => Observable.FromAsync(token => _mediator.Publish(notification, token)).Subscribe(observer));

        private readonly IMediator _mediator;
    }
}