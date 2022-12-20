using ApplicationState.Mediator;

namespace ApplicationState.Application.Background
{
    public abstract class BackgroundHandler : ApplicationStateHandler<StopApplicationEvent>, IBackgroundHandler { }
}