using System;
using ApplicationState.Machine.Events;
using ApplicationState.Mediator;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ApplicationState.Machine.Tests.State.Mediator
{
    public class ApplicationStateMediatorTests
    {
        [Fact]
        public void GivenInitializeEvent_WhenPublish_ThenHandled()
        {
            // Given
            var output = new Logger();
            var sut = new ServiceCollection()
                .AddSingleton(output)
                .AddTransient(typeof(IApplicationStateHandler<InitializeApplicationEvent>), typeof(ApplicationStateHandler<InitializeApplicationEvent>))
                .AddTransient<IMediator, MediatR.Mediator>()
                .AddTransient<IApplicationStateMediator, ApplicationStateMediator>()
                .AddMediatR(configuration => configuration.Using<ApplicationStateMediator>(), typeof(ApplicationStateMediator), typeof(TestInitializeHandler))
                .BuildServiceProvider()
                .GetService<IApplicationStateMediator>();

            // When
            using var _ = sut?.Publish(new InitializeApplicationEvent(new Uri("//Root"))).Subscribe();

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

        [Fact]
        public void GivenStartEvent_WhenPublish_ThenHandled()
        {
            // Given
            var output = new Logger();
            var sut = new ServiceCollection()
                .AddSingleton(output)
                .AddTransient(typeof(IApplicationStateHandler<StartApplicationEvent>), typeof(ApplicationStateHandler<StartApplicationEvent>))
                .AddTransient<IMediator, MediatR.Mediator>()
                .AddTransient<IApplicationStateMediator, ApplicationStateMediator>()
                .AddMediatR(configuration => configuration.Using<ApplicationStateMediator>(), typeof(ApplicationStateMediator), typeof(TestInitializeHandler))
                .BuildServiceProvider()
                .GetService<IApplicationStateMediator>();

            // When
            using var _ = sut?.Publish(new StartApplicationEvent(new Uri("//Root"))).Subscribe();

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

        [Fact]
        public void GivenStopEvent_WhenPublish_ThenHandled()
        {
            // Given
            var output = new Logger();
            var sut = new ServiceCollection()
                .AddSingleton(output)
                .AddTransient(typeof(IApplicationStateHandler<StopApplicationEvent>), typeof(ApplicationStateHandler<StopApplicationEvent>))
                .AddTransient<IMediator, MediatR.Mediator>()
                .AddTransient<IApplicationStateMediator, ApplicationStateMediator>()
                .AddMediatR(configuration => configuration.Using<ApplicationStateMediator>(), typeof(ApplicationStateMediator), typeof(TestInitializeHandler))
                .BuildServiceProvider()
                .GetService<IApplicationStateMediator>();

            // When
            using var _ = sut?.Publish(new StopApplicationEvent(new Uri("//Root"))).Subscribe();

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

        [Fact]
        public void GivenResumeEvent_WhenPublish_ThenHandled()
        {
            // Given
            var output = new Logger();
            var sut = new ServiceCollection()
                .AddSingleton(output)
                .AddTransient(typeof(IApplicationStateHandler<ResumeApplicationEvent>), typeof(ApplicationStateHandler<ResumeApplicationEvent>))
                .AddTransient<IMediator, MediatR.Mediator>()
                .AddTransient<IApplicationStateMediator, ApplicationStateMediator>()
                .AddMediatR(configuration => configuration.Using<ApplicationStateMediator>(), typeof(ApplicationStateMediator), typeof(TestInitializeHandler))
                .BuildServiceProvider()
                .GetService<IApplicationStateMediator>();

            // When
            using var _ = sut?.Publish(new ResumeApplicationEvent(new Uri("//Root"))).Subscribe();

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
    }
}