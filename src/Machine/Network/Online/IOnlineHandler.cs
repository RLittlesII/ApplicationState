using ApplicationState.Mediator;

namespace ApplicationState.Machine.Network.Online
{
    public interface IOnlineHandler : IApplicationStateHandler<GainedSignalEvent>  { }
}