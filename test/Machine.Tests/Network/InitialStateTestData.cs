using System.Collections;
using System.Collections.Generic;
using ApplicationState.Machine.Network;

namespace ApplicationState.Machine.Tests.Network;

internal class InitialStateTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { NetworkMachineTrigger.Connected, NetworkMachineState.Online };
        yield return new object[] { NetworkMachineTrigger.Disconnected, NetworkMachineState.Offline };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}