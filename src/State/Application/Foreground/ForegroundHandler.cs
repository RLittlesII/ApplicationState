using ApplicationState.Mediator;

namespace ApplicationState.Application.Foreground
{
    public abstract class ForegroundHandler : ApplicationStateHandler<StartApplicationEvent>, IForegroundHandler { }
}