using ApplicationState.Mediator;

namespace ApplicationState.Machine.Foreground
{
    public abstract class ResumeHandler : ApplicationStateHandler<ResumeApplicationEvent>, IResumeHandler { }
}