using ApplicationState.Mediator;

namespace ApplicationState.Machine.Background
{
    public interface IBackgroundHandler : IApplicationStateHandler<StopApplicationEvent> { }
}