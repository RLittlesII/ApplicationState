using System;
using ApplicationState.Mediator;

namespace ApplicationState.Machine.Events
{
    public abstract record ApplicationStateEvent(Uri Current) : IStateEvent;
}