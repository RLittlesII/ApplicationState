using System;
using ApplicationState.Application.Background;
using ApplicationState.Application.Foreground;
using ApplicationState.Application.Initialize;
using ApplicationState.Mediator;
using ApplicationState.Network.Offline;
using ApplicationState.Network.Online;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace State.Tests.Mediator
{
    public class ApplicationStateMediatorTests
    {
        [Theory]
        [ClassData(typeof(ApplicationStateMediatorTestData))]
        public void GivenInitializeEvent_WhenNotify_ThenHandled(IServiceProvider serviceProvider)
        {
            // Given
            var output = serviceProvider.GetService<Results>();
            var sut = serviceProvider
                .GetService<IApplicationStateMediator>();

            // When
            using var _ = sut?.Notify(new InitializeApplicationEvent()).Subscribe();

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
            var output = serviceProvider.GetService<Results>();
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
            var output = serviceProvider.GetService<Results>();
            var sut = serviceProvider
                .GetService<IApplicationStateMediator>();

            // When
            using var _ = sut?.Notify(new StopApplicationEvent()).Subscribe();

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
            var output = serviceProvider.GetService<Results>();
            var sut = serviceProvider
                .GetService<IApplicationStateMediator>();

            // When
            using var _ = sut?.Notify(new ResumeApplicationEvent()).Subscribe();

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
            var output = serviceProvider.GetService<Results>();
            var sut = serviceProvider.GetService<IApplicationStateMediator>();

            // When
            using var _ = sut?.Notify(new GainedSignalEvent()).Subscribe();

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
            var output = serviceProvider.GetService<Results>();
            var sut = serviceProvider.GetService<IApplicationStateMediator>();

            // When
            using var _ = sut?.Notify(new LostSignalEvent()).Subscribe();

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