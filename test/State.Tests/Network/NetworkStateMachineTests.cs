using System.Reactive;
using System.Reactive.Linq;
using FluentAssertions;
using NSubstitute;
using State.Application;
using State.Mediator;
using State.Network;
using State.Network.Offline;
using State.Network.Online;
using State.Tests.Application;
using Xunit;

namespace State.Tests.Network;

public class NetworkStateMachineTests
{
    [Fact]
    public void GivenInitial_WhenConnect_ThenOnline()
    {
        // Given
        NetworkStateMachine sut = new NetworkStateMachineFixture();

        // When
        sut.Connect(new GainedSignalEvent());

        // Then
        sut.State
            .Should()
            .Be(NetworkMachineState.Online);
    }

    [Fact]
    public void GivenInitial_WhenDisconnect_ThenOffline()
    {
        // Given
        NetworkStateMachine sut = new NetworkStateMachineFixture();

        // When
        sut.Disconnect(new LostSignalEvent());

        // Then
        sut.State
            .Should()
            .Be(NetworkMachineState.Offline);
    }

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
    public void GivenOffline_WhenConnect_ThenMediatorPublished()
    {
        // Given
        var mediator = Substitute.For<IApplicationStateMediator>();
        mediator.Notify(Arg.Any<ApplicationStateEvent>()).Returns(Observable.Return(Unit.Default));
        NetworkStateMachine sut = new NetworkStateMachineFixture().WithMediator(mediator).WithState(NetworkMachineState.Offline);

        // When
        sut.Connect(new GainedSignalEvent());

        // Then
        mediator.Received().Notify(Arg.Any<GainedSignalEvent>());
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
        mediator.Received().Notify(Arg.Any<LostSignalEvent>());
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

    [Theory]
    [ClassData(typeof(InitialStateTestData))]
    public void GivenInitial_WhenFired_ThenTransitioned(NetworkMachineTrigger machineTrigger,
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