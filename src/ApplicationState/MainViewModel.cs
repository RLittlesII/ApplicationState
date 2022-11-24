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
            Initialize = ReactiveCommand.Create(() =>
                stateMachine.Initialize(new InitializeApplicationEvent(new Uri("//NavigationPage"))));
            Start = ReactiveCommand.Create(() =>
                stateMachine.Start(new StartApplicationEvent(new Uri("//NavigationPage"))));
            Stop = ReactiveCommand.Create(() =>
                stateMachine.Stop(new StopApplicationEvent(new Uri("//NavigationPage"))));
            Offline = ReactiveCommand.Create(() =>
                stateMachine.Disconnect(new LostSignalEvent(new Uri("//NavigationPage"))));
            Online = ReactiveCommand.Create(() =>
                stateMachine.Connect(new GainedSignalEvent(new Uri("//NavigationPage"))));

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
        public ReactiveCommand<Unit, Unit> Start { get; }
        public ReactiveCommand<Unit, Unit> Stop { get; }
        public ReactiveCommand<Unit, Unit> Offline { get; }
        public ReactiveCommand<Unit, Unit> Online { get; }

        public Machine.ApplicationState CurrentState { [ObservableAsProperty] get; }
        public string Exceptions { [ObservableAsProperty]  get; }
    }
}