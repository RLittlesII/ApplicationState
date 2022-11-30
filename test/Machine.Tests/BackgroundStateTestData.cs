using System.Collections;
using System.Collections.Generic;

namespace ApplicationState.Machine.Tests
{
    internal class BackgroundStateTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { ApplicationMachineTrigger.Start, ApplicationMachineState.Foreground };
            yield return new object[] { ApplicationMachineTrigger.Stop, ApplicationMachineState.Background };
            yield return new object[] { ApplicationMachineTrigger.Connected, ApplicationMachineState.Background };
            yield return new object[] { ApplicationMachineTrigger.Disconnected, ApplicationMachineState.Background };
            yield return new object[] { ApplicationMachineTrigger.Deeplink, ApplicationMachineState.Foreground };
            yield return new object[] { ApplicationMachineTrigger.Notification, ApplicationMachineState.Foreground };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class ForegroundStateTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { ApplicationMachineTrigger.Start, ApplicationMachineState.Foreground };
            yield return new object[] { ApplicationMachineTrigger.Stop, ApplicationMachineState.Background };
            yield return new object[] { ApplicationMachineTrigger.Connected, ApplicationMachineState.Online };
            yield return new object[] { ApplicationMachineTrigger.Disconnected, ApplicationMachineState.Offline };
            yield return new object[] { ApplicationMachineTrigger.Deeplink, ApplicationMachineState.Foreground };
            yield return new object[] { ApplicationMachineTrigger.Notification, ApplicationMachineState.Foreground };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class InitialStateTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { ApplicationMachineTrigger.Start, ApplicationMachineState.Foreground };
            yield return new object[] { ApplicationMachineTrigger.Stop, ApplicationMachineState.Background };
            yield return new object[] { ApplicationMachineTrigger.Connected, ApplicationMachineState.Online };
            yield return new object[] { ApplicationMachineTrigger.Disconnected, ApplicationMachineState.Offline };
            yield return new object[] { ApplicationMachineTrigger.Deeplink, ApplicationMachineState.Initial };
            yield return new object[] { ApplicationMachineTrigger.Notification, ApplicationMachineState.Initial };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class OnlineStateTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { ApplicationMachineTrigger.Start, ApplicationMachineState.Foreground };
            yield return new object[] { ApplicationMachineTrigger.Stop, ApplicationMachineState.Background };
            yield return new object[] { ApplicationMachineTrigger.Connected, ApplicationMachineState.Online };
            yield return new object[] { ApplicationMachineTrigger.Disconnected, ApplicationMachineState.Offline };
            yield return new object[] { ApplicationMachineTrigger.Deeplink, ApplicationMachineState.Foreground };
            yield return new object[] { ApplicationMachineTrigger.Notification, ApplicationMachineState.Foreground };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class OfflineStateTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { ApplicationMachineTrigger.Start, ApplicationMachineState.Foreground };
            yield return new object[] { ApplicationMachineTrigger.Stop, ApplicationMachineState.Offline };
            yield return new object[] { ApplicationMachineTrigger.Connected, ApplicationMachineState.Online };
            yield return new object[] { ApplicationMachineTrigger.Disconnected, ApplicationMachineState.Offline };
            yield return new object[] { ApplicationMachineTrigger.Deeplink, ApplicationMachineState.Offline };
            yield return new object[] { ApplicationMachineTrigger.Notification, ApplicationMachineState.Offline };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}