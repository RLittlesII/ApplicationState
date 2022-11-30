using ApplicationState.Mediator;

namespace ApplicationState.Machine.Background
{
    public abstract class BackgroundHandler : ApplicationStateHandler<StopApplicationEvent>, IBackgroundHandler { }
}