using System;
using ApplicationState.Machine.Application;

namespace ApplicationState.Machine
{
    /// <summary>
    /// Interface representing <see cref="ApplicationStateEvent"/> stream.
    /// </summary>
    public interface IApplicationStateEventGenerator : IObservable<ApplicationStateEvent>
    {
    }
}