using System;

namespace ApplicationState.Machine.Events
{
    public record GainedSignalEvent(Uri Current) : ApplicationStateEvent(Current);
}