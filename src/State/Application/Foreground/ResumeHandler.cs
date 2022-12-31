using State.Mediator;

namespace State.Application.Foreground
{
    public abstract class ResumeHandler : ApplicationStateHandler<ResumeApplicationEvent>, IResumeHandler { }
}