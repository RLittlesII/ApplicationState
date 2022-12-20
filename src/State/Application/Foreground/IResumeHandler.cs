using ApplicationState.Mediator;

namespace ApplicationState.Application.Foreground
{
    public interface IResumeHandler : IApplicationStateHandler<ResumeApplicationEvent>  { }
}