using State.Mediator;

namespace State.Application.Foreground
{
    public interface IResumeHandler : IApplicationStateHandler<ResumeApplicationEvent>  { }
}