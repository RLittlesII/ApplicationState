using ApplicationState.Mediator;

namespace ApplicationState.Machine.Application.Foreground
{
    public abstract class ResumeHandler : ApplicationStateHandler<ResumeApplicationEvent>, IResumeHandler { }
}