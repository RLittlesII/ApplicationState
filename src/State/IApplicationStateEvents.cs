using System;
using ApplicationState.Application;

namespace ApplicationState
{
    /// <summary>
    /// Interface representing <see cref="ApplicationStateEvent"/> stream.
    /// </summary>
    public interface IApplicationStateEvents : IObservable<ApplicationStateEvent>
    {
    }
}