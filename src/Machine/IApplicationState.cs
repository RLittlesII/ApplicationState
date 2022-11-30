using System;

namespace ApplicationState.Machine
{
    /// <summary>
    /// Interface representing <see cref="ApplicationStateEvent"/> stream.
    /// </summary>
    public interface IApplicationState : IObservable<ApplicationStateEvent>
    {
    }
}