using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Machine;
using ApplicationState.Machine.Background;
using ApplicationState.Machine.Foreground;
using ApplicationState.Machine.Initialize;
using ApplicationState.Machine.Offline;
using ApplicationState.Machine.Online;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ApplicationState
{
    public class MainViewModel : ReactiveObject
    {
        public MainViewModel(ApplicationStatelessMachine stateMachine)
        {
            Initialize = ReactiveCommand.Create(() =>
                stateMachine.Initialize(new InitializeApplicationEvent()));
            Offline = ReactiveCommand.Create(() =>
                stateMachine.Disconnect(new LostSignalEvent()));
            Online = ReactiveCommand.Create(() =>
                stateMachine.Connect(new GainedSignalEvent()));
            Start = ReactiveCommand.Create(() =>
                stateMachine.Start(new StartApplicationEvent()));
            Stop = ReactiveCommand.Create(() =>
                stateMachine.Stop(new StopApplicationEvent()));

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

        public Machine.ApplicationMachineState CurrentState { [ObservableAsProperty] get; }
    }
}