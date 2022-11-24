using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Machine.Events;
using ApplicationState.Mediator;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace ApplicationState.Machine.Tests.State
{
    public class ApplicationStatelessMachineTests
    {
        [Theory]
        [ClassData(typeof(InitialStateTestData))]
        public void GivenInitial_WhenFired_ThenTransitioned(ApplicationEventTrigger eventTrigger,
            ApplicationState transitionedState)
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture();

            // When
            sut.Fire(eventTrigger);

            // Then
            sut.State
                .Should()
                .Be(transitionedState);
        }

        [Theory]
        [ClassData(typeof(BackgroundStateTestData))]
        public void GivenBackground_WhenFired_ThenTransitioned(ApplicationEventTrigger eventTrigger,
            ApplicationState transitionedState)
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithState(ApplicationState.Background);

            // When
            sut.Fire(eventTrigger);

            // Then
            sut.State
                .Should()
                .Be(transitionedState);
        }

        [Theory]
        [ClassData(typeof(ForegroundStateTestData))]
        public void GivenForeground_WhenFired_ThenTransitioned(ApplicationEventTrigger eventTrigger,
            ApplicationState transitionedState)
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithState(ApplicationState.Foreground);

            // When
            sut.Fire(eventTrigger);

            // Then
            sut.State
                .Should()
                .Be(transitionedState);
        }

        [Theory]
        [ClassData(typeof(OnlineStateTestData))]
        public void GivenOnline_WhenFired_ThenTransitioned(ApplicationEventTrigger eventTrigger,
            ApplicationState transitionedState)
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithState(ApplicationState.Online);

            // When
            sut.Fire(eventTrigger);

            // Then
            sut.State
                .Should()
                .Be(transitionedState);
        }

        [Theory]
        [ClassData(typeof(OfflineStateTestData))]
        public void GivenOffline_WhenFired_ThenTransitioned(ApplicationEventTrigger eventTrigger,
            ApplicationState transitionedState)
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithState(ApplicationState.Offline);

            // When
            sut.Fire(eventTrigger);

            // Then
            sut.State
                .Should()
                .Be(transitionedState);
        }

        [Fact]
        public void Given_WhenInitialize_ThenForeground()
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture();

            // When
            sut.Initialize(new InitializeApplicationEvent(new Uri("//Root")));

            // Then
            sut.State
                .Should()
                .Be(ApplicationState.Foreground);
        }

        [Fact]
        public void Given_WhenInitialize_ThenMediatorPublished()
        {
            // Given
            var mediator = Substitute.For<IApplicationStateMediator>();
            mediator.Publish(Arg.Any<ApplicationStateEvent>()).Returns(Observable.Return(Unit.Default));
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithMediator(mediator);

            // When
            sut.Initialize(new InitializeApplicationEvent(new Uri("//Root")));

            // Then
            mediator.Received().Publish(Arg.Any<InitializeApplicationEvent>());
        }

        [Fact]
        public void Given_WhenStart_ThenForeground()
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture();

            // When
            sut.Start(new StartApplicationEvent(new Uri("//Root")));

            // Then
            sut.State
                .Should()
                .Be(ApplicationState.Foreground);
        }

        [Fact]
        public void Given_WhenStart_ThenMediatorPublished()
        {
            // Given
            var mediator = Substitute.For<IApplicationStateMediator>();
            mediator.Publish(Arg.Any<ApplicationStateEvent>()).Returns(Observable.Return(Unit.Default));
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithMediator(mediator);

            // When
            sut.Start(new StartApplicationEvent(new Uri("//Root")));

            // Then
            mediator.Received().Publish(Arg.Any<StartApplicationEvent>());
        }

        [Fact]
        public void Given_WhenStop_ThenBackground()
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture();

            // When
            sut.Stop(new StopApplicationEvent(new Uri("//Root")));

            // Then
            sut.State
                .Should()
                .Be(ApplicationState.Background);
        }

        [Fact]
        public void Given_WhenStop_ThenMediatorPublished()
        {
            // Given
            var mediator = Substitute.For<IApplicationStateMediator>();
            mediator.Publish(Arg.Any<StopApplicationEvent>()).Returns(Observable.Return(Unit.Default));
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithState(ApplicationState.Foreground).WithMediator(mediator);

            // When
            sut.Stop(new StopApplicationEvent(new Uri("//Root")));

            // Then
            mediator.Received().Publish(Arg.Any<StopApplicationEvent>());
        }

        [Fact]
        public void Given_WhenResume_ThenForeground()
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture();

            // When
            sut.Resume(new ResumeApplicationEvent(new Uri("//Root")));

            // Then
            sut.State
                .Should()
                .Be(ApplicationState.Foreground);
        }

        [Fact]
        public void Given_WhenResume_ThenMediatorPublished()
        {
            // Given
            var mediator = Substitute.For<IApplicationStateMediator>();
            mediator.Publish(Arg.Any<ApplicationStateEvent>()).Returns(Observable.Return(Unit.Default));
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithMediator(mediator);

            // When
            sut.Resume(new ResumeApplicationEvent(new Uri("//Root")));

            // Then
            mediator.Received().Publish(Arg.Any<ResumeApplicationEvent>());
        }

        [Fact]
        public void GivenOffline_WhenConnect_ThenOnline()
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithState(ApplicationState.Offline);

            // When
            sut.Connect(new ConnectivityChangedEventFixture());

            // Then
            sut.State
                .Should()
                .Be(ApplicationState.Online);
        }

        [Fact]
        public void Given_WhenConnect_ThenMediatorPublished()
        {
            // Given
            var mediator = Substitute.For<IApplicationStateMediator>();
            mediator.Publish(Arg.Any<ApplicationStateEvent>()).Returns(Observable.Return(Unit.Default));
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithMediator(mediator).WithState(ApplicationState.Offline);

            // When
            sut.Connect(new ConnectivityChangedEventFixture());

            // Then
            // mediator.Received().Publish(Arg.Any<ResumeApplicationEvent>());
        }

        [Fact]
        public void GivenOnline_WhenDisconnect_TheOffline()
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithState(ApplicationState.Online);

            // When
            sut.Disconnect(new ConnectivityChangedEventFixture());

            // Then
            sut.State
                .Should()
                .Be(ApplicationState.Offline);
        }

        [Fact]
        public void GivenOnline_WhenDisconnect_ThenMediatorPublished()
        {
            // Given
            var mediator = Substitute.For<IApplicationStateMediator>();
            mediator.Publish(Arg.Any<ApplicationStateEvent>()).Returns(Observable.Return(Unit.Default));
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithMediator(mediator).WithState(ApplicationState.Online);

            // When
            sut.Disconnect(new ConnectivityChangedEventFixture());

            // Then
            // mediator.Received().Publish(Arg.Any<ResumeApplicationEvent>());
        }
    }
}