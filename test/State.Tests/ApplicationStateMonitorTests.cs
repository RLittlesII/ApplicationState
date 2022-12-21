using System;
using ApplicationState;
using ApplicationState.Application.Foreground;
using ApplicationState.Application.Initialize;
using ApplicationState.Network.Offline;
using ApplicationState.Network.Online;
using FluentAssertions;
using Xunit;

namespace State.Tests
{
    public class ApplicationStateMonitorTests
    {
        [Fact]
        public void GivenApplicationEvents_WhenInitial_ThenDefaultApplicationState()
        {
            // Given, When
            ApplicationState.ApplicationState? result = null;
            var applicationState = new ApplicationStateEventsMock();
            ApplicationStateMonitor sut = new ApplicationStateMonitorFixture().WithState(applicationState);
            sut.State.Subscribe(actual => result = actual);

            // Then
            result.Should().Be(ApplicationState.ApplicationState.Default);
        }

        [Fact]
        public void GivenApplicationEvents_WhenInitializedWithSignal_ThenApplicationStateCorrect()
        {
            // Given
            ApplicationState.ApplicationState? result = null;
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
            ApplicationState.ApplicationState? result = null;
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
                .As<ApplicationState.ApplicationState>()
                .Connected
                .Should()
                .BeTrue();
        }

        [Fact]
        public void GivenConnectivity_WhenHasNoConnection_ThenOffline()
        {
            // Given
            ApplicationState.ApplicationState? result = null;
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
                .As<ApplicationState.ApplicationState>()
                .Connected
                .Should()
                .BeFalse();
        }
    }
}