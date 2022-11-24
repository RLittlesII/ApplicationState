using ApplicationState.Machine.Events;
using ApplicationState.Mediator;

namespace ApplicationState.Machine.Handlers
{
    public abstract class OfflineHandler : ApplicationStateHandler<LostSignalEvent>, IOfflineHandler { }
}