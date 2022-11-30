using ApplicationState.Mediator;

namespace ApplicationState.Machine.Foreground
{
    public interface IForegroundHandler : IApplicationStateHandler<StartApplicationEvent>  { }
}