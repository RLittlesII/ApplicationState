using NSubstitute;
using Rocket.Surgery.Extensions.Testing.Fixtures;
using State.Application;
using State.Mediator;

namespace State.Tests.Application
{
    internal sealed class ApplicationStatelessMachineFixture : ITestFixtureBuilder
    {
        public static implicit operator ApplicationStateMachine(ApplicationStatelessMachineFixture fixture) => fixture.Build();
        public ApplicationStatelessMachineFixture WithState(ApplicationMachineState machineState) => this.With(ref _machineState, machineState);
        public ApplicationStatelessMachineFixture WithMediator(IApplicationStateMediator mediator) => this.With(ref _mediator, mediator);
        private ApplicationStateMachine Build() => new ApplicationStateMachine(_mediator, _machineState);

        private ApplicationMachineState _machineState = ApplicationMachineState.Initial;
        private IApplicationStateMediator _mediator = Substitute.For<IApplicationStateMediator>();
    }
}