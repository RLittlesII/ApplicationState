using ApplicationState.Mediator;

namespace ApplicationState.Machine.Initialize
{
    public interface IInitializationHandler : IApplicationStateHandler<InitializeApplicationEvent>  { }
}