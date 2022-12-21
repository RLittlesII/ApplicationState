using ApplicationState.Mediator;
using ApplicationState.Network;
using NSubstitute;
using Rocket.Surgery.Extensions.Testing.Fixtures;

namespace State.Tests.Application;

internal sealed class NetworkStateMachineFixture : ITestFixtureBuilder
{
    public static implicit operator NetworkStateMachine(NetworkStateMachineFixture fixture) => fixture.Build();
    public NetworkStateMachineFixture WithState(NetworkMachineState machineState) => this.With(ref _machineState, machineState);
    public NetworkStateMachineFixture WithMediator(IApplicationStateMediator mediator) => this.With(ref _mediator, mediator);
    private NetworkStateMachine Build() => new NetworkStateMachine(_mediator, _machineState);

    private NetworkMachineState _machineState = NetworkMachineState.Initial;
    private IApplicationStateMediator _mediator = Substitute.For<IApplicationStateMediator>();
}