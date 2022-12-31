using System;
using State.Application;

namespace State
{
    /// <summary>
    /// Interface representing <see cref="ApplicationStateEvent"/> stream.
    /// </summary>
    public interface IApplicationStateEvents : IObservable<ApplicationStateEvent>
    {
    }
}