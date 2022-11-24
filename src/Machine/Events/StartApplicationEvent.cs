using System;

namespace ApplicationState.Machine.Events
{
    public record StartApplicationEvent(Uri Current) : ApplicationStateEvent(Current);
}