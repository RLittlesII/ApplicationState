using System.Collections;
using System.Collections.Generic;
using State.Application;

namespace State.Tests.Application;

internal class InitialStateTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { ApplicationMachineTrigger.Start, ApplicationMachineState.Foreground };
        yield return new object[] { ApplicationMachineTrigger.Stop, ApplicationMachineState.Background };
        yield return new object[] { ApplicationMachineTrigger.Deeplink, ApplicationMachineState.Initial };
        yield return new object[] { ApplicationMachineTrigger.Notification, ApplicationMachineState.Initial };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}