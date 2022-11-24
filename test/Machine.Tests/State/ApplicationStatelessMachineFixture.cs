using ApplicationState.Mediator;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Rocket.Surgery.Extensions.Testing.Fixtures;

namespace ApplicationState.Machine.Tests.State
{
    internal sealed class ApplicationStatelessMachineFixture : ITestFixtureBuilder
    {
        public static implicit operator ApplicationStatelessMachine(ApplicationStatelessMachineFixture fixture) => fixture.Build();
        public ApplicationStatelessMachineFixture WithLogger(ILogger<ApplicationStatelessMachine> logger) => this.With(ref _logger, logger);
        public ApplicationStatelessMachineFixture WithState(ApplicationState state) => this.With(ref _state, state);
        public ApplicationStatelessMachineFixture WithMediator(IApplicationStateMediator mediator) => this.With(ref _mediator, mediator);
        private ApplicationStatelessMachine Build() => new ApplicationStatelessMachine(_mediator, _logger, _state);

        private ApplicationState _state = ApplicationState.Initial;
        private IApplicationStateMediator _mediator = Substitute.For<IApplicationStateMediator>();
        private ILogger<ApplicationStatelessMachine> _logger = Substitute.For<ILogger<ApplicationStatelessMachine>>();
    }
}