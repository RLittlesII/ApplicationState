using System;

namespace ApplicationState.Machine.Events
{
    public record ResumeApplicationEvent(Uri Current) : ApplicationStateEvent(Current);
}