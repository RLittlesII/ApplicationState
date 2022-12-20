using System;
using ApplicationState.Application.Foreground;
using ApplicationState.Application.Initialize;
using ApplicationState.Network.Offline;
using ApplicationState.Network.Online;
using FluentAssertions;
using Xunit;

namespace ApplicationState.Machine.Tests
{
    public class ApplicationStateMonitorTests
    {
        [Fact]
        public void GivenApplicationEvents_When_Then()
        {
            // Given
            ApplicationState? result = null;
            var applicationState = new ApplicationStateEventsMock();
            ApplicationStateMonitor sut = new ApplicationStateMonitorFixture().WithState(applicationState);
            sut.State.Subscribe(actual => result = actual);
            // When
            applicationState.Notify(new InitializeApplicationEvent(), new GainedSignalEvent());

            // Then
            result.Should().NotBeNull();
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
                .NotBeNull();

            result
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
                .NotBeNull();

            result
                .Connected
                .Should()
                .BeFalse();
        }
    }
}