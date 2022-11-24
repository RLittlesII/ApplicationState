using System;

namespace ApplicationState.Machine.Events
{
    public record InitializeApplicationEvent(Uri Current) : ApplicationStateEvent(Current);
}