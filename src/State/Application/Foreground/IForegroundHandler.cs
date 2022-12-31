using State.Mediator;

namespace State.Application.Foreground
{
    public interface IForegroundHandler : IApplicationStateHandler<StartApplicationEvent>  { }
}