using System.Collections.Generic;

namespace ApplicationState.Machine.Tests.State.Mediator
{
    public class Logger
    {
        public IList<string> Messages { get; } = new List<string>();
    }
}