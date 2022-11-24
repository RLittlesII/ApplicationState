using ApplicationState.Machine.Events;
using ApplicationState.Mediator;

namespace ApplicationState.Machine.Handlers
{
    public abstract class BackgroundHandler : ApplicationStateHandler<StopApplicationEvent>, IBackgroundHandler { }
}