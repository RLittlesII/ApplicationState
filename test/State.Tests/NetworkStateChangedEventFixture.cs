using System.Linq;
using ApplicationState.Network;
using Rocket.Surgery.Extensions.Testing.Fixtures;

namespace State.Tests
{
    internal sealed class NetworkStateChangedEventFixture : ITestFixtureBuilder
    {
        public static implicit operator NetworkStateChangedEvent(NetworkStateChangedEventFixture fixture) => fixture.Build();

        public NetworkStateChangedEventFixture WithDefault() => WithDefaultAccess().WithDefaultProfiles();
        public NetworkStateChangedEventFixture WithOffline() => this.With(ref _networkAccess, NetworkAccess.None).WithProfiles(ConnectionProfile.Unknown);

        public NetworkStateChangedEventFixture WithDefaultAccess() => this.With(ref _networkAccess, NetworkAccess.Internet);

        public NetworkStateChangedEventFixture WithDefaultProfiles() => WithProfiles(
            ConnectionProfile.Bluetooth,
            ConnectionProfile.Ethernet,
            ConnectionProfile.WiFi
        );

        public NetworkStateChangedEventFixture WithProfiles(params ConnectionProfile[] profiles) => this.With(ref _profiles, profiles);

        private NetworkStateChangedEvent Build() => new NetworkStateChangedEvent(_networkAccess, _profiles.ToArray());

        private NetworkAccess _networkAccess = NetworkAccess.Internet;
        private ConnectionProfile[] _profiles = { ConnectionProfile.WiFi };
    }
}