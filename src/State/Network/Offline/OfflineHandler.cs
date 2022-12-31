using State.Mediator;

namespace State.Network.Offline
{
    public abstract class OfflineHandler : ApplicationStateHandler<LostSignalEvent>, IOfflineHandler { }
}