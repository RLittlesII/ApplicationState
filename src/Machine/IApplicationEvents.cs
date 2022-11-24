using System;
using ApplicationState.Machine.Events;

namespace ApplicationState.Machine
{
    /// <summary>
    /// Interface representing <see cref="ApplicationStateEvent"/> stream.
    /// </summary>
    public interface IApplicationEvents : IObservable<ApplicationStateEvent>
    {
    }
}