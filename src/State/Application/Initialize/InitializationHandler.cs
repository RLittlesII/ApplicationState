using State.Mediator;

namespace State.Application.Initialize
{
    public abstract class InitializationHandler : ApplicationStateHandler<InitializeApplicationEvent>, IInitializationHandler { }
}