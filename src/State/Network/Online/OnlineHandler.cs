using State.Mediator;

namespace State.Network.Online
{
    public abstract class OnlineHandler : ApplicationStateHandler<GainedSignalEvent>, IOnlineHandler { }
}