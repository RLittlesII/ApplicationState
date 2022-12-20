using ApplicationState.Mediator;

namespace ApplicationState.Application.Background
{
    public interface IBackgroundHandler : IApplicationStateHandler<StopApplicationEvent> { }
}