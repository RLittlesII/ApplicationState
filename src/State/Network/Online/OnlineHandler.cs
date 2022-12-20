using ApplicationState.Mediator;

namespace ApplicationState.Network.Online
{
    public abstract class OnlineHandler : ApplicationStateHandler<GainedSignalEvent>, IOnlineHandler { }
}