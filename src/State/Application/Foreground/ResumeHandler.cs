using ApplicationState.Mediator;

namespace ApplicationState.Application.Foreground
{
    public abstract class ResumeHandler : ApplicationStateHandler<ResumeApplicationEvent>, IResumeHandler { }
}