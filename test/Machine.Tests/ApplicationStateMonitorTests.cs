using System;
using ApplicationState.Machine.Initialize;
using ApplicationState.Machine.Offline;
using ApplicationState.Machine.Online;
using FluentAssertions;
using NSubstitute;
using Rocket.Surgery.Extensions.Testing.Fixtures;
using Xunit;

namespace ApplicationState.Machine.Tests
{
    public class ApplicationStateMonitorTests
    {
        [Fact]
        public void GivenApplicationEvents_When_Then()
        {
            // Given
            ApplicationMachineState? result = null;
            var applicationState = new ApplicationStateMock();
            ApplicationStateMonitor sut = new ApplicationStateMonitorFixture().WithState(applicationState);
            sut.StateChanged.Subscribe(actual => result = actual);
            // When
            applicationState.Notify(new InitializeApplicationEvent(new Uri("//current")));

            // Then
            result.Should().NotBeNull();
        }

        [Fact]
        public void GivenConnectivity_WhenHasConnection_ThenOnline()
        {
            // Given
            ApplicationMachineState? result = null;
            var applicationState = new ApplicationStateMock();
            ApplicationStateMonitor sut = new ApplicationStateMonitorFixture().WithState(applicationState);
            sut.StateChanged.Subscribe(actual => result = actual);

            // When
            applicationState.Notify(new GainedSignalEvent());

            // Then
            result
               .Should()
               .NotBeNull()
               .And
               .Subject
               .Should()
               .Be(ApplicationMachineState.Online);
        }

        [Fact]
        public void GivenConnectivity_WhenHasNoConnection_ThenOffline()
        {
            // Given
            ApplicationMachineState? result = null;
            var applicationState = new ApplicationStateMock();
            ApplicationStateMonitor sut = new ApplicationStateMonitorFixture().WithState(applicationState);
            using var _ = sut.StateChanged.Subscribe(actual => result = actual);

            // When
            applicationState.Notify(new LostSignalEvent());

            // Then
            result
               .Should()
               .NotBeNull()
               .And
               .Subject
               .Should()
               .Be(ApplicationMachineState.Offline);
        }
    }

    internal sealed class ApplicationStateMonitorFixture : ITestFixtureBuilder
    {
        public static implicit operator ApplicationStateMonitor(ApplicationStateMonitorFixture fixture) => fixture.Build();
        public ApplicationStateMonitorFixture WithState(IApplicationState applicationState) => this.With(ref _applicationState, applicationState);
        public ApplicationStateMonitorFixture WithState(INetworkState networkState) => this.With(ref _networkState, networkState);
        private ApplicationStateMonitor Build() => new ApplicationStateMonitor(_applicationState, _applicationStatelessMachine);

        private INetworkState _networkState = Substitute.For<INetworkState>();
        private IApplicationState _applicationState = Substitute.For<IApplicationState>();
        private ApplicationStatelessMachine _applicationStatelessMachine = new ApplicationStatelessMachineFixture();
    }
}