using System.Linq;
using ApplicationState.Machine.Events;
using Rocket.Surgery.Extensions.Testing.Fixtures;

namespace ApplicationState.Machine.Tests.State
{
    internal sealed class ConnectivityChangedEventFixture : ITestFixtureBuilder
    {
        public static implicit operator ConnectivityChangedEvent(ConnectivityChangedEventFixture fixture) => fixture.Build();

        public ConnectivityChangedEventFixture WithDefault() => WithDefaultAccess().WithDefaultProfiles();
        public ConnectivityChangedEventFixture WithOffline() => this.With(ref _networkAccess, NetworkAccess.None).WithProfiles(ConnectionProfile.Unknown);

        public ConnectivityChangedEventFixture WithDefaultAccess() => this.With(ref _networkAccess, NetworkAccess.Internet);

        public ConnectivityChangedEventFixture WithDefaultProfiles() => WithProfiles(
            ConnectionProfile.Bluetooth,
            ConnectionProfile.Ethernet,
            ConnectionProfile.WiFi
        );

        public ConnectivityChangedEventFixture WithProfiles(params ConnectionProfile[] profiles) => this.With(ref _profiles, profiles);

        private ConnectivityChangedEvent Build() => new ConnectivityChangedEvent(_networkAccess, _profiles.ToArray());

        private NetworkAccess _networkAccess = NetworkAccess.Internet;
        private ConnectionProfile[] _profiles = { ConnectionProfile.WiFi };
    }
}