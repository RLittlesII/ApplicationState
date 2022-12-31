using State.Mediator;

namespace State.Application.Background
{
    public abstract class BackgroundHandler : ApplicationStateHandler<StopApplicationEvent>, IBackgroundHandler { }
}