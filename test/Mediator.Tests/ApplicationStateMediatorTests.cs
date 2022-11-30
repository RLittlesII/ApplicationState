using System;
using ApplicationState.Machine.Background;
using ApplicationState.Machine.Foreground;
using ApplicationState.Machine.Initialize;
using ApplicationState.Machine.Offline;
using ApplicationState.Machine.Online;

namespace ApplicationState.Mediator.Tests
{
    public class ApplicationStateMediatorTests
    {
        [Theory]
        [ClassData(typeof(ApplicationStateMediatorTestData))]
        public void GivenInitializeEvent_WhenNotify_ThenHandled(IServiceProvider serviceProvider)
        {
            // Given
            var output = serviceProvider.GetService<Logger>();
            var sut = serviceProvider
                .GetService<IApplicationStateMediator>();

            // When
            using var _ = sut?.Notify(new InitializeApplicationEvent(new Uri("//Root"))).Subscribe();

            // Then
            output
                .Messages
                .Should()
                .NotBeEmpty()
                .And
                .Subject
                .Should()
                .ContainSingle(x => x == "Initialized");
        }

        [Theory]
        [ClassData(typeof(ApplicationStateMediatorTestData))]
        public void GivenStartEvent_WhenNotify_ThenHandled(IServiceProvider serviceProvider)
        {
            // Given
            var output = serviceProvider.GetService<Logger>();
            var sut = serviceProvider
                .GetService<IApplicationStateMediator>();

            // When
            using var _ = sut?.Notify(new StartApplicationEvent()).Subscribe();

            // Then
            output
                .Messages
                .Should()
                .NotBeEmpty()
                .And
                .Subject
                .Should()
                .HaveCount(2)
                .And
                .Subject
                .Should()
                .Contain(x => x == "Start Ping")
                .And
                .Subject
                .Should()
                .Contain(x => x == "Start Pong");
        }

        [Theory]
        [ClassData(typeof(ApplicationStateMediatorTestData))]
        public void GivenStopEvent_WhenNotify_ThenHandled(IServiceProvider serviceProvider)
        {
            // Given
            var output = serviceProvider.GetService<Logger>();
            var sut = serviceProvider
                .GetService<IApplicationStateMediator>();

            // When
            using var _ = sut?.Notify(new StopApplicationEvent(new Uri("//Root"))).Subscribe();

            // Then
            output
                .Messages
                .Should()
                .NotBeEmpty()
                .And
                .Subject
                .Should()
                .HaveCount(2)
                .And
                .Subject
                .Should()
                .Contain(x => x == "Stop Ping")
                .And
                .Subject
                .Should()
                .Contain(x => x == "Stop Pong");
        }


        [Theory]
        [ClassData(typeof(ApplicationStateMediatorTestData))]
        public void GivenResumeEvent_WhenNotify_ThenHandled(IServiceProvider serviceProvider)
        {
            // Given
            var output = serviceProvider.GetService<Logger>();
            var sut = serviceProvider
                .GetService<IApplicationStateMediator>();

            // When
            using var _ = sut?.Notify(new ResumeApplicationEvent(new Uri("//Root"))).Subscribe();

            // Then
            output
                .Messages
                .Should()
                .NotBeEmpty()
                .And
                .Subject
                .Should()
                .HaveCount(2)
                .And
                .Subject
                .Should()
                .Contain(x => x == "Resume Ping")
                .And
                .Subject
                .Should()
                .Contain(x => x == "Resume Pong");
        }

        [Theory]
        [ClassData(typeof(ApplicationStateMediatorTestData))]
        public void GivenGainedSignal_WhenNotify_ThenHandled(IServiceProvider serviceProvider)
        {
            // Given
            var output = serviceProvider.GetService<Logger>();
            var sut = serviceProvider.GetService<IApplicationStateMediator>();

            // When
            using var _ = sut?.Notify(new GainedSignalEvent(new Uri("//Root"))).Subscribe();

            // Then
            output
                .Messages
                .Should()
                .NotBeEmpty()
                .And
                .Subject
                .Should()
                .HaveCount(2)
                .And
                .Subject
                .Should()
                .Contain(x => x == "Gained Ping")
                .And
                .Subject
                .Should()
                .Contain(x => x == "Gained Pong");
        }

        [Theory]
        [ClassData(typeof(ApplicationStateMediatorTestData))]
        public void GivenLostSignal_WhenNotify_ThenHandled(IServiceProvider serviceProvider)
        {
            // Given
            var output = serviceProvider.GetService<Logger>();
            var sut = serviceProvider.GetService<IApplicationStateMediator>();

            // When
            using var _ = sut?.Notify(new LostSignalEvent(new Uri("//Root"))).Subscribe();

            // Then
            output
                .Messages
                .Should()
                .NotBeEmpty()
                .And
                .Subject
                .Should()
                .HaveCount(2)
                .And
                .Subject
                .Should()
                .Contain(x => x == "Lost Ping")
                .And
                .Subject
                .Should()
                .Contain(x => x == "Lost Pong");
        }
    }
}