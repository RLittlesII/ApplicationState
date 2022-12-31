using NSubstitute;
using Rocket.Surgery.Extensions.Testing.Fixtures;
using State.Application;
using State.Network;
using State.Tests.Application;

namespace State.Tests;

internal sealed class ApplicationStateMonitorFixture : ITestFixtureBuilder
{
    public static implicit operator ApplicationStateMonitor(ApplicationStateMonitorFixture fixture) => fixture.Build();
    public ApplicationStateMonitorFixture WithState(IApplicationStateEvents applicationStateEvents) => this.With(ref _applicationStateEvents, applicationStateEvents);
    private ApplicationStateMonitor Build() => new ApplicationStateMonitor(_applicationStateEvents, _applicationStateMachine, _networkStateMachine);

    private INetworkState _networkState = Substitute.For<INetworkState>();
    private IApplicationStateEvents _applicationStateEvents = Substitute.For<IApplicationStateEvents>();
    private ApplicationStateMachine _applicationStateMachine = new ApplicationStatelessMachineFixture();
    private NetworkStateMachine _networkStateMachine = new NetworkStateMachineFixture();
}