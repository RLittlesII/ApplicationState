using ApplicationState.Machine.Events;

namespace ApplicationState.Machine.Tests.State
{
    internal sealed class ConnectivityChangedEventFixture : ITestFixtureBuilder
    {
        public static implicit operator NetworkStateChangedEvent(ConnectivityChangedEventFixture fixture) => fixture.Build();

        public ConnectivityChangedEventFixture WithDefault() => WithDefaultAccess().WithDefaultProfiles();
        public ConnectivityChangedEventFixture WithOffline() => this.With(ref _networkAccess, NetworkAccess.None).WithProfiles(ConnectionProfile.Unknown);

        public ConnectivityChangedEventFixture WithDefaultAccess() => this.With(ref _networkAccess, NetworkAccess.Internet);

        public ConnectivityChangedEventFixture WithDefaultProfiles() => WithProfiles(
            ConnectionProfile.Bluetooth,
            ConnectionProfile.Ethernet,
            ConnectionProfile.WiFi
        );

        public ConnectivityChangedEventFixture WithProfiles(params ConnectionProfile[] profiles) => this.With(ref _profiles, profiles);

        private NetworkStateChangedEvent Build() => new NetworkStateChangedEvent(_networkAccess, _profiles.ToArray());

        private NetworkAccess _networkAccess = NetworkAccess.Internet;
        private ConnectionProfile[] _profiles = { ConnectionProfile.WiFi };
    }
}