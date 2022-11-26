using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Machine;
using ApplicationState.Machine.Events;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ApplicationState
{
    public class MainViewModel : ReactiveObject
    {
        public MainViewModel(ApplicationStatelessMachine stateMachine)
        {
            var navigationPage = new Uri("//NavigationPage");

            Initialize = ReactiveCommand.Create(() =>
                stateMachine.Initialize(new InitializeApplicationEvent(navigationPage)));
            Offline = ReactiveCommand.Create(() =>
                stateMachine.Disconnect(new LostSignalEvent(navigationPage)));
            Online = ReactiveCommand.Create(() =>
                stateMachine.Connect(new GainedSignalEvent(navigationPage)));
            Start = ReactiveCommand.Create(() =>
                stateMachine.Start(new StartApplicationEvent(navigationPage)));
            Stop = ReactiveCommand.Create(() =>
                stateMachine.Stop(new StopApplicationEvent(navigationPage)));

            stateMachine
                .StateChanged
                .ToPropertyEx(this, x => x.CurrentState);

            stateMachine
                .UnhandledExceptions
                .Select(value => Interactions.UnhandledTransitions.Handle(value))
                .Switch()
                .Subscribe();
        }


        public ReactiveCommand<Unit, Unit> Initialize { get; }
        public ReactiveCommand<Unit, Unit> Offline { get; }
        public ReactiveCommand<Unit, Unit> Online { get; }
        public ReactiveCommand<Unit, Unit> Start { get; }
        public ReactiveCommand<Unit, Unit> Stop { get; }

        public Machine.ApplicationState CurrentState { [ObservableAsProperty] get; }
    }
}