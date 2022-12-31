using State.Mediator;

namespace State.Application.Initialize
{
    public interface IInitializationHandler : IApplicationStateHandler<InitializeApplicationEvent>  { }
}