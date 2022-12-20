using ApplicationState.Mediator;

namespace ApplicationState.Application.Foreground
{
    public interface IForegroundHandler : IApplicationStateHandler<StartApplicationEvent>  { }
}