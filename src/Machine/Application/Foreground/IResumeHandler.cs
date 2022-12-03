using ApplicationState.Mediator;

namespace ApplicationState.Machine.Application.Foreground
{
    public interface IResumeHandler : IApplicationStateHandler<ResumeApplicationEvent>  { }
}