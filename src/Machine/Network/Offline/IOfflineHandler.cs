using ApplicationState.Mediator;

namespace ApplicationState.Machine.Network.Offline
{
    public interface IOfflineHandler : IApplicationStateHandler<LostSignalEvent>  { }
}