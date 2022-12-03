using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Machine.Application;
using ApplicationState.Machine.Network;
using ApplicationState.Machine.Network.Offline;
using ApplicationState.Machine.Network.Online;
using ApplicationState.Machine.Tests.Application;
using ApplicationState.Machine.Tests.Network.TestData;
using ApplicationState.Mediator;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace ApplicationState.Machine.Tests.Network;

public class NetworkStateMachineTests
{
    [Fact]
    public void GivenOffline_WhenConnect_ThenOnline()
    {
        // Given
        NetworkStateMachine sut = new NetworkStateMachineFixture().WithState(NetworkMachineState.Offline);

        // When
        sut.Connect(new GainedSignalEvent());

        // Then
        sut.State
            .Should()
            .Be(NetworkMachineState.Online);
    }

    [Fact]
    public void Given_WhenConnect_ThenMediatorPublished()
    {
        // Given
        var mediator = Substitute.For<IApplicationStateMediator>();
        mediator.Notify(Arg.Any<ApplicationStateEvent>()).Returns(Observable.Return(Unit.Default));
        NetworkStateMachine sut = new NetworkStateMachineFixture().WithMediator(mediator).WithState(NetworkMachineState.Offline);

        // When
        sut.Connect(new GainedSignalEvent());

        // Then
        // mediator.Received().Publish(Arg.Any<ResumeApplicationEvent>());
    }

    [Fact]
    public void GivenOnline_WhenDisconnect_TheOffline()
    {
        // Given
        NetworkStateMachine sut = new NetworkStateMachineFixture().WithState(NetworkMachineState.Online);

        // When
        sut.Disconnect(new LostSignalEvent());

        // Then
        sut.State
            .Should()
            .Be(NetworkMachineState.Offline);
    }

    [Fact]
    public void GivenOnline_WhenDisconnect_ThenMediatorPublished()
    {
        // Given
        var mediator = Substitute.For<IApplicationStateMediator>();
        mediator.Notify(Arg.Any<ApplicationStateEvent>()).Returns(Observable.Return(Unit.Default));
        NetworkStateMachine sut = new NetworkStateMachineFixture().WithMediator(mediator).WithState(NetworkMachineState.Online);

        // When
        sut.Disconnect(new LostSignalEvent());

        // Then
        // mediator.Received().Publish(Arg.Any<ResumeApplicationEvent>());
    }

    [Theory]
    [ClassData(typeof(OnlineStateTestData))]
    public void GivenOnline_WhenFired_ThenTransitioned(NetworkMachineTrigger machineTrigger,
        NetworkMachineState transitionedState)
    {
        // Given
        NetworkStateMachine sut = new NetworkStateMachineFixture().WithState(NetworkMachineState.Online);

        // When
        sut.Fire(machineTrigger);

        // Then
        sut.State
            .Should()
            .Be(transitionedState);
    }

    [Theory]
    [ClassData(typeof(OfflineStateTestData))]
    public void GivenOffline_WhenFired_ThenTransitioned(NetworkMachineTrigger machineTrigger,
        NetworkMachineState transitionedState)
    {
        // Given
        NetworkStateMachine sut = new NetworkStateMachineFixture().WithState(NetworkMachineState.Offline);

        // When
        sut.Fire(machineTrigger);

        // Then
        sut.State
            .Should()
            .Be(transitionedState);
    }
}