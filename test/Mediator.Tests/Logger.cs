using System.Collections.Generic;

namespace ApplicationState.Mediator.Tests
{
    public class Logger
    {
        public IList<string> Messages { get; } = new List<string>();
    }
}