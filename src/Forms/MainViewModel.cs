using System;
using System.Reactive;
using System.Reactive.Linq;
using ApplicationState.Application;
using ApplicationState.Application.Background;
using ApplicationState.Application.Foreground;
using ApplicationState.Application.Initialize;
using ApplicationState.Network;
using ApplicationState.Network.Offline;
using ApplicationState.Network.Online;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ApplicationState
{
    public class MainViewModel : ReactiveObject
    {
        // NOTE: [rlittlesii: December 03, 2022] The statemachines are here so we can explicitly move state for the sample.
        public MainViewModel(ApplicationStateMonitor applicationStateMonitor,
            ApplicationStateMachine applicationStateMachine,
            NetworkStateMachine networkStateMachine)
        {
            Initialize = ReactiveCommand.Create(() =>
                applicationStateMachine.Initialize(new InitializeApplicationEvent()));
            Offline = ReactiveCommand.Create(() =>
                networkStateMachine.Disconnect(new LostSignalEvent()));
            Online = ReactiveCommand.Create(() =>
                networkStateMachine.Connect(new GainedSignalEvent()));
            Start = ReactiveCommand.Create(() =>
                applicationStateMachine.Start(new StartApplicationEvent()));
            Stop = ReactiveCommand.Create(() =>
                applicationStateMachine.Stop(new StopApplicationEvent()));

            applicationStateMonitor
                .State
                .ToPropertyEx(this, viewModel => viewModel.CurrentState);

            applicationStateMachine
                .UnhandledExceptions
                .Merge(networkStateMachine.UnhandledExceptions)
                .Select(value => Interactions.UnhandledTransitions.Handle(value))
                .Switch()
                .Subscribe();
        }


        public ReactiveCommand<Unit, Unit> Initialize { get; }

        public ReactiveCommand<Unit, Unit> Offline { get; }

        public ReactiveCommand<Unit, Unit> Online { get; }

        public ReactiveCommand<Unit, Unit> Start { get; }

        public ReactiveCommand<Unit, Unit> Stop { get; }

        public ApplicationState CurrentState { [ObservableAsProperty] get; }
    }
}