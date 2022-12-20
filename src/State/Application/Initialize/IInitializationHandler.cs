using ApplicationState.Mediator;

namespace ApplicationState.Application.Initialize
{
    public interface IInitializationHandler : IApplicationStateHandler<InitializeApplicationEvent>  { }
}