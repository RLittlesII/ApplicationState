using ApplicationState.Mediator;

namespace ApplicationState.Network.Online
{
    public interface IOnlineHandler : IApplicationStateHandler<GainedSignalEvent>  { }
}