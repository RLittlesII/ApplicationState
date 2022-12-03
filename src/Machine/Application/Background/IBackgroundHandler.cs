using ApplicationState.Mediator;

namespace ApplicationState.Machine.Application.Background
{
    public interface IBackgroundHandler : IApplicationStateHandler<StopApplicationEvent> { }
}