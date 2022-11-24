using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using ApplicationState.Machine.Events;
using FluentAssertions;
using NSubstitute;
using Rocket.Surgery.Extensions.Testing.Fixtures;
using Xunit;

namespace ApplicationState.Machine.Tests.State
{
    public class ApplicationStateMachineTests
    {
        [Fact]
        public void GivenApplicationEvents_When_Then()
        {
            // Given
            ApplicationState? result = null;
            var events = new ApplicationEventsMock();
            ApplicationStateMonitor sut = new ApplicationStateMachineFixture().WithEvents(events);
            sut.StateChanged.Subscribe(actual => result = actual);
            // When
            events.Notify(new InitializeApplicationEvent(new Uri("//current")));

            // Then
            result.Should().NotBeNull();
        }

        [Fact]
        public void GivenConnectivity_WhenHasConnection_ThenOnline()
        {
            // Given
            ApplicationState? result = null;
            var connectivity = new ApplicationConnectivityMock();
            ApplicationStateMonitor sut = new ApplicationStateMachineFixture().WithConnectivity(connectivity);
            sut.StateChanged.Subscribe(actual => result = actual);

            // When
            connectivity.Notify(new ConnectivityChangedEvent(NetworkAccess.Internet, ConnectionProfile.WiFi));

            // Then
            result
               .Should()
               .NotBeNull()
               .And
               .Subject
               .Should()
               .Be(ApplicationState.Online);
        }

        [Fact]
        public void GivenConnectivity_WhenHasNoConnection_ThenOffline()
        {
            // Given
            ApplicationState? result = null;
            var connectivity = new ApplicationConnectivityMock();
            ApplicationStateMonitor sut = new ApplicationStateMachineFixture().WithConnectivity(connectivity);
            using var _ = sut.StateChanged.Subscribe(actual => result = actual);

            // When
            connectivity.Notify(new ConnectivityChangedEventFixture().WithOffline());

            // Then
            result
               .Should()
               .NotBeNull()
               .And
               .Subject
               .Should()
               .Be(ApplicationState.Offline);
        }
    }

    internal sealed class ApplicationStateMachineFixture : ITestFixtureBuilder
    {
        public static implicit operator ApplicationStateMonitor(ApplicationStateMachineFixture fixture) => fixture.Build();
        public ApplicationStateMachineFixture WithEvents(IApplicationEvents applicationEvents) => this.With(ref _applicationEvents, applicationEvents);
        public ApplicationStateMachineFixture WithConnectivity(IApplicationConnectivity applicationConnectivity) => this.With(ref _applicationConnectivity, applicationConnectivity);
        private ApplicationStateMonitor Build() => new ApplicationStateMonitor(_applicationEvents, _applicationConnectivity, _applicationStatelessMachine);

        private IApplicationEvents _applicationEvents = Substitute.For<IApplicationEvents>();
        private IApplicationConnectivity _applicationConnectivity = Substitute.For<IApplicationConnectivity>();
        private ApplicationStatelessMachine _applicationStatelessMachine = new ApplicationStatelessMachineFixture();
    }

    public class ApplicationConnectivityMock : IApplicationConnectivity
    {
        public NetworkAccess NetworkAccess { get; }

        public IReadOnlyList<ConnectionProfile> Profiles { get; }

        public void Notify(ConnectivityChangedEvent args) => _connectivityChanged.OnNext(args);

        public IDisposable Subscribe(IObserver<ConnectivityChangedEvent> observer) => _connectivityChanged.Subscribe(observer);

        private readonly Subject<ConnectivityChangedEvent> _connectivityChanged = new Subject<ConnectivityChangedEvent>();
    }
}