using ApplicationState.Mediator;

namespace ApplicationState.Machine.Online
{
    public abstract class OnlineHandler : ApplicationStateHandler<GainedSignalEvent>, IOnlineHandler { }
}