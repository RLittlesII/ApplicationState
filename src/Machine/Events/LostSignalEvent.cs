using System;

namespace ApplicationState.Machine.Events
{
    public record LostSignalEvent(Uri Current) : ApplicationStateEvent(Current);
}