using System.Collections;
using System.Collections.Generic;

namespace ApplicationState.Machine.Tests.State
{
    internal class BackgroundStateTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { ApplicationEventTrigger.Start, ApplicationState.Foreground };
            yield return new object[] { ApplicationEventTrigger.Stop, ApplicationState.Background };
            yield return new object[] { ApplicationEventTrigger.Connected, ApplicationState.Background };
            yield return new object[] { ApplicationEventTrigger.Disconnected, ApplicationState.Background };
            yield return new object[] { ApplicationEventTrigger.Deeplink, ApplicationState.Foreground };
            yield return new object[] { ApplicationEventTrigger.Notification, ApplicationState.Foreground };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class ForegroundStateTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { ApplicationEventTrigger.Start, ApplicationState.Foreground };
            yield return new object[] { ApplicationEventTrigger.Stop, ApplicationState.Background };
            yield return new object[] { ApplicationEventTrigger.Connected, ApplicationState.Online };
            yield return new object[] { ApplicationEventTrigger.Disconnected, ApplicationState.Offline };
            yield return new object[] { ApplicationEventTrigger.Deeplink, ApplicationState.Foreground };
            yield return new object[] { ApplicationEventTrigger.Notification, ApplicationState.Foreground };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class InitialStateTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { ApplicationEventTrigger.Start, ApplicationState.Foreground };
            yield return new object[] { ApplicationEventTrigger.Stop, ApplicationState.Background };
            yield return new object[] { ApplicationEventTrigger.Connected, ApplicationState.Online };
            yield return new object[] { ApplicationEventTrigger.Disconnected, ApplicationState.Offline };
            yield return new object[] { ApplicationEventTrigger.Deeplink, ApplicationState.Initial };
            yield return new object[] { ApplicationEventTrigger.Notification, ApplicationState.Initial };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class OnlineStateTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { ApplicationEventTrigger.Start, ApplicationState.Foreground };
            yield return new object[] { ApplicationEventTrigger.Stop, ApplicationState.Background };
            yield return new object[] { ApplicationEventTrigger.Connected, ApplicationState.Online };
            yield return new object[] { ApplicationEventTrigger.Disconnected, ApplicationState.Offline };
            yield return new object[] { ApplicationEventTrigger.Deeplink, ApplicationState.Foreground };
            yield return new object[] { ApplicationEventTrigger.Notification, ApplicationState.Foreground };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class OfflineStateTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { ApplicationEventTrigger.Start, ApplicationState.Foreground };
            yield return new object[] { ApplicationEventTrigger.Stop, ApplicationState.Offline };
            yield return new object[] { ApplicationEventTrigger.Connected, ApplicationState.Online };
            yield return new object[] { ApplicationEventTrigger.Disconnected, ApplicationState.Offline };
            yield return new object[] { ApplicationEventTrigger.Deeplink, ApplicationState.Offline };
            yield return new object[] { ApplicationEventTrigger.Notification, ApplicationState.Offline };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}