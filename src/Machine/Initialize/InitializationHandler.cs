using ApplicationState.Mediator;

namespace ApplicationState.Machine.Initialize
{
    public abstract class InitializationHandler : ApplicationStateHandler<InitializeApplicationEvent>, IInitializationHandler { }
}