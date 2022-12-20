using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Application;
using ApplicationState.Application.Background;
using ApplicationState.Application.Foreground;
using ApplicationState.Application.Initialize;
using ApplicationState.Mediator;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace ApplicationState.Machine.Tests.Application
{
    public class ApplicationStateMachineTests
    {
        [Theory]
        [ClassData(typeof(InitialStateTestData))]
        public void GivenInitial_WhenFired_ThenTransitioned(ApplicationMachineTrigger machineTrigger,
            ApplicationMachineState transitionedState)
        {
            // Given
            ApplicationStateMachine sut = new ApplicationStatelessMachineFixture();

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
            ApplicationStateMachine sut = new ApplicationStatelessMachineFixture().WithState(ApplicationMachineState.Background);

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
            ApplicationStateMachine sut = new ApplicationStatelessMachineFixture().WithState(ApplicationMachineState.Foreground);

            // When
            sut.Fire(machineTrigger);

            // Then
            sut.State
                .Should()
                .Be(transitionedState);
        }

        [Fact]
        public void GivenInitial_WhenInitialize_ThenForeground()
        {
            // Given
            ApplicationStateMachine sut = new ApplicationStatelessMachineFixture();

            // When
            sut.Initialize(new InitializeApplicationEvent());

            // Then
            sut.State
                .Should()
                .Be(ApplicationMachineState.Foreground);
        }

        [Fact]
        public void GivenInitial_WhenInitialize_ThenMediatorPublished()
        {
            // Given
            var mediator = Substitute.For<IApplicationStateMediator>();
            mediator.Notify(Arg.Any<ApplicationStateEvent>()).Returns(Observable.Return(Unit.Default));
            ApplicationStateMachine sut = new ApplicationStatelessMachineFixture().WithMediator(mediator);

            // When
            sut.Initialize(new InitializeApplicationEvent());

            // Then
            mediator.Received().Notify(Arg.Any<InitializeApplicationEvent>());
        }

        [Fact]
        public void GivenInitial_WhenStart_ThenForeground()
        {
            // Given
            ApplicationStateMachine sut = new ApplicationStatelessMachineFixture();

            // When
            sut.Start(new StartApplicationEvent());

            // Then
            sut.State
                .Should()
                .Be(ApplicationMachineState.Foreground);
        }

        [Fact]
        public void GivenInitial_WhenStart_ThenMediatorPublished()
        {
            // Given
            var mediator = Substitute.For<IApplicationStateMediator>();
            mediator.Notify(Arg.Any<ApplicationStateEvent>()).Returns(Observable.Return(Unit.Default));
            ApplicationStateMachine sut = new ApplicationStatelessMachineFixture().WithMediator(mediator);

            // When
            sut.Start(new StartApplicationEvent());

            // Then
            mediator.Received().Notify(Arg.Any<StartApplicationEvent>());
        }

        [Fact]
        public void GivenInitial_WhenStop_ThenBackground()
        {
            // Given
            ApplicationStateMachine sut = new ApplicationStatelessMachineFixture();

            // When
            sut.Stop(new StopApplicationEvent());

            // Then
            sut.State
                .Should()
                .Be(ApplicationMachineState.Background);
        }

        [Fact]
        public void GivenForeground_WhenStop_ThenMediatorPublished()
        {
            // Given
            var mediator = Substitute.For<IApplicationStateMediator>();
            mediator.Notify(Arg.Any<StopApplicationEvent>()).Returns(Observable.Return(Unit.Default));
            ApplicationStateMachine sut = new ApplicationStatelessMachineFixture().WithState(ApplicationMachineState.Foreground).WithMediator(mediator);

            // When
            sut.Stop(new StopApplicationEvent());

            // Then
            mediator.Received().Notify(Arg.Any<StopApplicationEvent>());
        }

        [Fact]
        public void GivenInitial_WhenResume_ThenForeground()
        {
            // Given
            ApplicationStateMachine sut = new ApplicationStatelessMachineFixture();

            // When
            sut.Resume(new ResumeApplicationEvent());

            // Then
            sut.State
                .Should()
                .Be(ApplicationMachineState.Foreground);
        }

        [Fact]
        public void GivenInitial_WhenResume_ThenMediatorPublished()
        {
            // Given
            var mediator = Substitute.For<IApplicationStateMediator>();
            mediator.Notify(Arg.Any<ApplicationStateEvent>()).Returns(Observable.Return(Unit.Default));
            ApplicationStateMachine sut = new ApplicationStatelessMachineFixture().WithMediator(mediator);

            // When
            sut.Resume(new ResumeApplicationEvent());

            // Then
            mediator.Received().Notify(Arg.Any<ResumeApplicationEvent>());
        }
    }
}