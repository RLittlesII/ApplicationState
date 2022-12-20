using ApplicationState.Mediator;

namespace ApplicationState.Network.Offline
{
    public abstract class OfflineHandler : ApplicationStateHandler<LostSignalEvent>, IOfflineHandler { }
}