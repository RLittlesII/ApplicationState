using ApplicationState.Mediator;

namespace ApplicationState.Machine.Offline
{
    public abstract class OfflineHandler : ApplicationStateHandler<LostSignalEvent>, IOfflineHandler { }
}