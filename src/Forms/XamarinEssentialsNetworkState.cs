using System;
using System.Linq;
using System.Reactive.Linq;
using ApplicationState.Network;
using Xamarin.Essentials;
using NetworkAccess = ApplicationState.Network.NetworkAccess;
using ConnectionProfile = ApplicationState.Network.ConnectionProfile;

namespace ApplicationState
{
    public class XamarinEssentialsNetworkState : INetworkState
    {
        public XamarinEssentialsNetworkState()
        {
            _networkStateChanges =
                Observable.FromEvent<EventHandler<ConnectivityChangedEventArgs>, ConnectivityChangedEventArgs>(handler =>
                {
                    void Handler(object sender, ConnectivityChangedEventArgs eventArgs) => handler(eventArgs);
                    return Handler;
                },
                eventHandler => Connectivity.ConnectivityChanged += eventHandler,
                eventHandler => Connectivity.ConnectivityChanged -= eventHandler)
                .Select(connectivityChanged => new NetworkStateChangedEvent(Access(connectivityChanged), Profiles(connectivityChanged)));

            NetworkAccess Access(ConnectivityChangedEventArgs x) =>
                x.NetworkAccess switch
                {
                    Xamarin.Essentials.NetworkAccess.Unknown => NetworkAccess.Unknown,
                    Xamarin.Essentials.NetworkAccess.None => NetworkAccess.None,
                    Xamarin.Essentials.NetworkAccess.Local => NetworkAccess.Local,
                    Xamarin.Essentials.NetworkAccess.ConstrainedInternet => NetworkAccess.ConstrainedInternet,
                    Xamarin.Essentials.NetworkAccess.Internet => NetworkAccess.Internet,
                    _ => throw new ArgumentOutOfRangeException()
                };

            ConnectionProfile[] Profiles(ConnectivityChangedEventArgs connectivityChange) =>
                connectivityChange
                    .ConnectionProfiles
                    .Select(connectionProfile => connectionProfile switch
                    {
                        Xamarin.Essentials.ConnectionProfile.Unknown => ConnectionProfile.Unknown,
                        Xamarin.Essentials.ConnectionProfile.Bluetooth => ConnectionProfile.Bluetooth,
                        Xamarin.Essentials.ConnectionProfile.Cellular => ConnectionProfile.Cellular,
                        Xamarin.Essentials.ConnectionProfile.Ethernet => ConnectionProfile.Ethernet,
                        Xamarin.Essentials.ConnectionProfile.WiFi => ConnectionProfile.WiFi,
                        _ => throw new ArgumentOutOfRangeException(nameof(connectionProfile), connectionProfile, null)
                    })
                    .ToArray();
        }

        /// <inheritdoc />
        public IDisposable Subscribe(IObserver<NetworkStateChangedEvent> observer) =>
            _networkStateChanges.Subscribe(observer);

        private readonly IObservable<NetworkStateChangedEvent> _networkStateChanges;
    }
}