using ApplicationState.Mediator;

namespace ApplicationState.Machine.Offline
{
    public interface IOfflineHandler : IApplicationStateHandler<LostSignalEvent>  { }
}