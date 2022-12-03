using ApplicationState.Mediator;

namespace ApplicationState.Machine.Application.Background
{
    public abstract class BackgroundHandler : ApplicationStateHandler<StopApplicationEvent>, IBackgroundHandler { }
}