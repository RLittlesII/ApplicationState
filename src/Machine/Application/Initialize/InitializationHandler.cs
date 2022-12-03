using ApplicationState.Mediator;

namespace ApplicationState.Machine.Application.Initialize
{
    public abstract class InitializationHandler : ApplicationStateHandler<InitializeApplicationEvent>, IInitializationHandler { }
}