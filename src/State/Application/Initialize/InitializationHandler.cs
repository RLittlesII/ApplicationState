using ApplicationState.Mediator;

namespace ApplicationState.Application.Initialize
{
    public abstract class InitializationHandler : ApplicationStateHandler<InitializeApplicationEvent>, IInitializationHandler { }
}