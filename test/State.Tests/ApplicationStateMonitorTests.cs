using System;
using FluentAssertions;
using State.Application.Foreground;
using State.Application.Initialize;
using State.Network.Offline;
using State.Network.Online;
using Xunit;

namespace State.Tests
{
    public class ApplicationStateMonitorTests
    {
        [Fact]
        public void GivenApplicationEvents_WhenInitial_ThenDefaultApplicationState()
        {
            // Given, When
            ApplicationState? result = null;
            var applicationState = new ApplicationStateEventsMock();
            ApplicationStateMonitor sut = new ApplicationStateMonitorFixture().WithState(applicationState);
            sut.State.Subscribe(actual => result = actual);

            // Then
            result.Should().Be(ApplicationState.Default);
        }

        [Fact]
        public void GivenApplicationEvents_WhenInitializedWithSignal_ThenApplicationStateCorrect()
        {
            // Given
            ApplicationState? result = null;
            var applicationState = new ApplicationStateEventsMock();
            ApplicationStateMonitor sut = new ApplicationStateMonitorFixture().WithState(applicationState);
            sut.State.Subscribe(actual => result = actual);

            // When
            applicationState.Notify(new InitializeApplicationEvent(), new GainedSignalEvent());

            // Then
            result
                .Foreground
                .Should()
                .BeTrue();

            result
                .Connected
                .Should()
                .BeTrue();
        }

        [Fact]
        public void GivenConnectivity_WhenGainedSignal_ThenOnline()
        {
            // Given
            ApplicationState? result = null;
            var applicationState = new ApplicationStateEventsMock();
            ApplicationStateMonitor sut = new ApplicationStateMonitorFixture().WithState(applicationState);
            sut.State.Subscribe(actual => result = actual);

            // When
            applicationState.Notify(new StartApplicationEvent(), new GainedSignalEvent());

            // Then
            result
                .Should()
                .NotBeNull()
                .And
                .Subject
                .As<ApplicationState>()
                .Connected
                .Should()
                .BeTrue();
        }

        [Fact]
        public void GivenConnectivity_WhenHasNoConnection_ThenOffline()
        {
            // Given
            ApplicationState? result = null;
            var applicationState = new ApplicationStateEventsMock();
            ApplicationStateMonitor sut = new ApplicationStateMonitorFixture().WithState(applicationState);
            using var _ = sut.State.Subscribe(actual => result = actual);

            // When
            applicationState.Notify(new LostSignalEvent(), new StartApplicationEvent());

            // Then
            result
                .Should()
                .NotBeNull()
                .And
                .Subject
                .As<ApplicationState>()
                .Connected
                .Should()
                .BeFalse();
        }
    }
}