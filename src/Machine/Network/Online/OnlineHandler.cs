using ApplicationState.Mediator;

namespace ApplicationState.Machine.Network.Online
{
    public abstract class OnlineHandler : ApplicationStateHandler<GainedSignalEvent>, IOnlineHandler { }
}