using ApplicationState.Mediator;

namespace ApplicationState.Network.Offline
{
    public interface IOfflineHandler : IApplicationStateHandler<LostSignalEvent>  { }
}