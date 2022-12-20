using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationState.Mediator;

namespace ApplicationState.Network
{
    public class NetworkStateChangedEvent : EventArgs, IStateEvent
    {
        public NetworkStateChangedEvent(NetworkAccess access, params ConnectionProfile[]  connectionProfiles)
        {
            NetworkAccess = access;
            ConnectionProfiles = connectionProfiles.ToList().AsReadOnly();
        }

        public NetworkAccess NetworkAccess { get; }

        public IReadOnlyList<ConnectionProfile> ConnectionProfiles { get; }

        public bool HasSignal() => HasValidProfile() && HasNetworkAccess();

        public bool Degraded() => NetworkAccess > NetworkAccess.None || ContainsProfile(ConnectionProfile.Unknown);

        public bool HasValidProfile() => ContainsProfile(ConnectionProfile.Bluetooth, ConnectionProfile.Cellular, ConnectionProfile.Ethernet, ConnectionProfile.WiFi);

        public override string ToString() => $"{nameof(NetworkAccess)}: {NetworkAccess}, " +
                                             $"{nameof(ConnectionProfiles)}: [{string.Join(", ", ConnectionProfiles)}]";

        private bool HasNetworkAccess() => NetworkAccess is NetworkAccess.Internet or NetworkAccess.Local or NetworkAccess.ConstrainedInternet;

        private bool ContainsProfile(params ConnectionProfile[] profiles) => ConnectionProfiles.Select(profile => profiles.Contains(profile)).Any(x => x);
    }
}