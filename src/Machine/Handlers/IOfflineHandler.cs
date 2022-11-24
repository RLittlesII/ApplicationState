using ApplicationState.Machine.Events;
using ApplicationState.Mediator;

namespace ApplicationState.Machine.Handlers
{
    public interface IOfflineHandler : IApplicationStateHandler<LostSignalEvent>  { }
}