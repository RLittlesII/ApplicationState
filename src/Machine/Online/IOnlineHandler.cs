using ApplicationState.Mediator;

namespace ApplicationState.Machine.Online
{
    public interface IOnlineHandler : IApplicationStateHandler<GainedSignalEvent>  { }
}