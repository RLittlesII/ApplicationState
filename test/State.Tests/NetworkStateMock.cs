using System;
using System.Reactive.Subjects;
using State.Network;

namespace State.Tests;

public class NetworkStateMock : INetworkState
{
    public void Notify(NetworkStateChangedEvent networkStateChangedEvent) => _notify.OnNext(networkStateChangedEvent);

    public IDisposable Subscribe(IObserver<NetworkStateChangedEvent> observer) => _notify.Subscribe(observer);

    private readonly Subject<NetworkStateChangedEvent> _notify = new Subject<NetworkStateChangedEvent>();
}