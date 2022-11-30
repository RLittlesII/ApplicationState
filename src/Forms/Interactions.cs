using System.Reactive;
using ReactiveUI;

namespace ApplicationState
{
    public static class Interactions
    {
        public static readonly Interaction<string, Unit> UnhandledTransitions =
            new Interaction<string, Unit>();
    }
}