using ApplicationState.Mediator;

namespace ApplicationState.Machine.Application.Foreground
{
    public interface IForegroundHandler : IApplicationStateHandler<StartApplicationEvent>  { }
}