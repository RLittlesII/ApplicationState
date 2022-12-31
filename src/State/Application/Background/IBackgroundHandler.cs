using State.Mediator;

namespace State.Application.Background
{
    public interface IBackgroundHandler : IApplicationStateHandler<StopApplicationEvent> { }
}