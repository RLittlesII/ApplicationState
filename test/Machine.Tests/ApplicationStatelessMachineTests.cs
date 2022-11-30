using System;
using ApplicationState.Machine.Background;
using ApplicationState.Machine.Foreground;
using ApplicationState.Machine.Initialize;
using ApplicationState.Machine.Offline;
using ApplicationState.Machine.Online;
using ApplicationState.Mediator;

namespace ApplicationState.Machine.Tests
{
    public class ApplicationStatelessMachineTests
    {
        [Theory]
        [ClassData(typeof(InitialStateTestData))]
        public void GivenInitial_WhenFired_ThenTransitioned(ApplicationMachineTrigger machineTrigger,
            ApplicationMachineState transitionedState)
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture();

            // When
            sut.Fire(machineTrigger);

            // Then
            sut.State
                .Should()
                .Be(transitionedState);
        }

        [Theory]
        [ClassData(typeof(BackgroundStateTestData))]
        public void GivenBackground_WhenFired_ThenTransitioned(ApplicationMachineTrigger machineTrigger,
            ApplicationMachineState transitionedState)
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithState(ApplicationMachineState.Background);

            // When
            sut.Fire(machineTrigger);

            // Then
            sut.State
                .Should()
                .Be(transitionedState);
        }

        [Theory]
        [ClassData(typeof(ForegroundStateTestData))]
        public void GivenForeground_WhenFired_ThenTransitioned(ApplicationMachineTrigger machineTrigger,
            ApplicationMachineState transitionedState)
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithState(ApplicationMachineState.Foreground);

            // When
            sut.Fire(machineTrigger);

            // Then
            sut.State
                .Should()
                .Be(transitionedState);
        }

        [Theory]
        [ClassData(typeof(OnlineStateTestData))]
        public void GivenOnline_WhenFired_ThenTransitioned(ApplicationMachineTrigger machineTrigger,
            ApplicationMachineState transitionedState)
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithState(ApplicationMachineState.Online);

            // When
            sut.Fire(machineTrigger);

            // Then
            sut.State
                .Should()
                .Be(transitionedState);
        }

        [Theory]
        [ClassData(typeof(OfflineStateTestData))]
        public void GivenOffline_WhenFired_ThenTransitioned(ApplicationMachineTrigger machineTrigger,
            ApplicationMachineState transitionedState)
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithState(ApplicationMachineState.Offline);

            // When
            sut.Fire(machineTrigger);

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
            sut.Initialize(new InitializeApplicationEvent());

            // Then
            sut.State
                .Should()
                .Be(ApplicationMachineState.Foreground);
        }

        [Fact]
        public void Given_WhenInitialize_ThenMediatorPublished()
        {
            // Given
            var mediator = Substitute.For<IApplicationStateMediator>();
            mediator.Notify(Arg.Any<ApplicationStateEvent>()).Returns(Observable.Return(Unit.Default));
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithMediator(mediator);

            // When
            sut.Initialize(new InitializeApplicationEvent());

            // Then
            mediator.Received().Notify(Arg.Any<InitializeApplicationEvent>());
        }

        [Fact]
        public void Given_WhenStart_ThenForeground()
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture();

            // When
            sut.Start(new StartApplicationEvent());

            // Then
            sut.State
                .Should()
                .Be(ApplicationMachineState.Foreground);
        }

        [Fact]
        public void Given_WhenStart_ThenMediatorPublished()
        {
            // Given
            var mediator = Substitute.For<IApplicationStateMediator>();
            mediator.Notify(Arg.Any<ApplicationStateEvent>()).Returns(Observable.Return(Unit.Default));
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithMediator(mediator);

            // When
            sut.Start(new StartApplicationEvent());

            // Then
            mediator.Received().Notify(Arg.Any<StartApplicationEvent>());
        }

        [Fact]
        public void Given_WhenStop_ThenBackground()
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture();

            // When
            sut.Stop(new StopApplicationEvent());

            // Then
            sut.State
                .Should()
                .Be(ApplicationMachineState.Background);
        }

        [Fact]
        public void Given_WhenStop_ThenMediatorPublished()
        {
            // Given
            var mediator = Substitute.For<IApplicationStateMediator>();
            mediator.Notify(Arg.Any<StopApplicationEvent>()).Returns(Observable.Return(Unit.Default));
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithState(ApplicationMachineState.Foreground).WithMediator(mediator);

            // When
            sut.Stop(new StopApplicationEvent());

            // Then
            mediator.Received().Notify(Arg.Any<StopApplicationEvent>());
        }

        [Fact]
        public void Given_WhenResume_ThenForeground()
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture();

            // When
            sut.Resume(new ResumeApplicationEvent());

            // Then
            sut.State
                .Should()
                .Be(ApplicationMachineState.Foreground);
        }

        [Fact]
        public void Given_WhenResume_ThenMediatorPublished()
        {
            // Given
            var mediator = Substitute.For<IApplicationStateMediator>();
            mediator.Notify(Arg.Any<ApplicationStateEvent>()).Returns(Observable.Return(Unit.Default));
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithMediator(mediator);

            // When
            sut.Resume(new ResumeApplicationEvent());

            // Then
            mediator.Received().Notify(Arg.Any<ResumeApplicationEvent>());
        }

        [Fact]
        public void GivenOffline_WhenConnect_ThenOnline()
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithState(ApplicationMachineState.Offline);

            // When
            sut.Connect(new GainedSignalEvent());

            // Then
            sut.State
                .Should()
                .Be(ApplicationMachineState.Online);
        }

        [Fact]
        public void Given_WhenConnect_ThenMediatorPublished()
        {
            // Given
            var mediator = Substitute.For<IApplicationStateMediator>();
            mediator.Notify(Arg.Any<ApplicationStateEvent>()).Returns(Observable.Return(Unit.Default));
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithMediator(mediator).WithState(ApplicationMachineState.Offline);

            // When
            sut.Connect(new GainedSignalEvent());

            // Then
            // mediator.Received().Publish(Arg.Any<ResumeApplicationEvent>());
        }

        [Fact]
        public void GivenOnline_WhenDisconnect_TheOffline()
        {
            // Given
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithState(ApplicationMachineState.Online);

            // When
            sut.Disconnect(new LostSignalEvent());

            // Then
            sut.State
                .Should()
                .Be(ApplicationMachineState.Offline);
        }

        [Fact]
        public void GivenOnline_WhenDisconnect_ThenMediatorPublished()
        {
            // Given
            var mediator = Substitute.For<IApplicationStateMediator>();
            mediator.Notify(Arg.Any<ApplicationStateEvent>()).Returns(Observable.Return(Unit.Default));
            ApplicationStatelessMachine sut = new ApplicationStatelessMachineFixture().WithMediator(mediator).WithState(ApplicationMachineState.Online);

            // When
            sut.Disconnect(new LostSignalEvent());

            // Then
            // mediator.Received().Publish(Arg.Any<ResumeApplicationEvent>());
        }
    }
}