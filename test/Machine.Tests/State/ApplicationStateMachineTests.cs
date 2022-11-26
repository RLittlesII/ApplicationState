using ApplicationState.Machine.Events;

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
            var connectivity = new NetworkStateMock();
            ApplicationStateMonitor sut = new ApplicationStateMachineFixture().WithConnectivity(connectivity);
            sut.StateChanged.Subscribe(actual => result = actual);

            // When
            connectivity.Notify(new NetworkStateChangedEvent(NetworkAccess.Internet, ConnectionProfile.WiFi));

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
            var connectivity = new NetworkStateMock();
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
        public ApplicationStateMachineFixture WithConnectivity(INetworkState networkState) => this.With(ref _networkState, networkState);
        private ApplicationStateMonitor Build() => new ApplicationStateMonitor(_applicationEvents, _networkState, _applicationStatelessMachine);

        private IApplicationEvents _applicationEvents = Substitute.For<IApplicationEvents>();
        private INetworkState _networkState = Substitute.For<INetworkState>();
        private ApplicationStatelessMachine _applicationStatelessMachine = new ApplicationStatelessMachineFixture();
    }

    public class NetworkStateMock : INetworkState
    {
        public void Notify(NetworkStateChangedEvent args) => _connectivityChanged.OnNext(args);

        public IDisposable Subscribe(IObserver<NetworkStateChangedEvent> observer) => _connectivityChanged.Subscribe(observer);

        private readonly Subject<NetworkStateChangedEvent> _connectivityChanged = new Subject<NetworkStateChangedEvent>();
    }
}