using ApplicationState.Mediator;
using NSubstitute;
using Rocket.Surgery.Extensions.Testing.Fixtures;

namespace ApplicationState.Machine.Tests
{
    internal sealed class ApplicationStatelessMachineFixture : ITestFixtureBuilder
    {
        public static implicit operator ApplicationStatelessMachine(ApplicationStatelessMachineFixture fixture) => fixture.Build();
        public ApplicationStatelessMachineFixture WithState(ApplicationMachineState machineState) => this.With(ref _machineState, machineState);
        public ApplicationStatelessMachineFixture WithMediator(IApplicationStateMediator mediator) => this.With(ref _mediator, mediator);
        private ApplicationStatelessMachine Build() => new ApplicationStatelessMachine(_mediator, _machineState);

        private ApplicationMachineState _machineState = ApplicationMachineState.Initial;
        private IApplicationStateMediator _mediator = Substitute.For<IApplicationStateMediator>();
    }
}