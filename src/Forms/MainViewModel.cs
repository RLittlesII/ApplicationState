using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Machine;
using ApplicationState.Machine.Application;
using ApplicationState.Machine.Application.Background;
using ApplicationState.Machine.Application.Foreground;
using ApplicationState.Machine.Application.Initialize;
using ApplicationState.Machine.Network;
using ApplicationState.Machine.Network.Offline;
using ApplicationState.Machine.Network.Online;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ApplicationState
{
    public class MainViewModel : ReactiveObject
    {
        public MainViewModel(ApplicationStateMachine stateMachine, NetworkStateMachine networkStateMachine)
        {
            Initialize = ReactiveCommand.Create(() =>
                stateMachine.Initialize(new InitializeApplicationEvent()));
            Offline = ReactiveCommand.Create(() =>
                networkStateMachine.Disconnect(new LostSignalEvent()));
            Online = ReactiveCommand.Create(() =>
                networkStateMachine.Connect(new GainedSignalEvent()));
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

        public ApplicationMachineState CurrentState { [ObservableAsProperty] get; }
    }
}