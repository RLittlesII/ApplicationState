using System;

namespace ApplicationState.Machine.Events
{
    public record StopApplicationEvent(Uri Current) : ApplicationStateEvent(Current);
}