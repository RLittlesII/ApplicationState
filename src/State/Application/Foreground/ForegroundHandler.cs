using State.Mediator;

namespace State.Application.Foreground
{
    public abstract class ForegroundHandler : ApplicationStateHandler<StartApplicationEvent>, IForegroundHandler { }
}