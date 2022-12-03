using ApplicationState.Mediator;

namespace ApplicationState.Machine.Network.Offline
{
    public abstract class OfflineHandler : ApplicationStateHandler<LostSignalEvent>, IOfflineHandler { }
}