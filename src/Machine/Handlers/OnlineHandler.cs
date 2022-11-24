using ApplicationState.Machine.Events;
using ApplicationState.Mediator;

namespace ApplicationState.Machine.Handlers
{
    public abstract class OnlineHandler : ApplicationStateHandler<GainedSignalEvent>, IOnlineHandler { }
}