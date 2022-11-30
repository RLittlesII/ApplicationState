using ApplicationState.Mediator;

namespace ApplicationState.Machine.Foreground
{
    public interface IResumeHandler : IApplicationStateHandler<ResumeApplicationEvent>  { }
}