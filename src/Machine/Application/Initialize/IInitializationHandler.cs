using ApplicationState.Mediator;

namespace ApplicationState.Machine.Application.Initialize
{
    public interface IInitializationHandler : IApplicationStateHandler<InitializeApplicationEvent>  { }
}