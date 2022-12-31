using State.Mediator;

namespace State.Network.Offline
{
    public interface IOfflineHandler : IApplicationStateHandler<LostSignalEvent>  { }
}