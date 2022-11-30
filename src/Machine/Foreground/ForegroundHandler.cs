using ApplicationState.Mediator;

namespace ApplicationState.Machine.Foreground
{
    public abstract class ForegroundHandler : ApplicationStateHandler<StartApplicationEvent>, IForegroundHandler { }
}