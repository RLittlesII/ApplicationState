using System;
using System.Reactive.Subjects;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ReactiveMarbles.ObservableEvents;
using State;
using State.Application;
using State.Application.Background;
using State.Application.Foreground;
using State.Application.Initialize;
using State.Mediator;
using State.Network;
using State.Network.Offline;
using State.Network.Online;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: GenerateStaticEventObservables(typeof(Xamarin.Essentials.Connectivity))]

namespace ApplicationState
{
    public partial class App : Xamarin.Forms.Application, IApplicationLifecycleState
    {
        public App()
        {
            InitializeComponent();

            var services =
                new ServiceCollection()
                    .AddSingleton<ApplicationStateMonitor>()
                    .AddSingleton<IApplicationStateEvents, ApplicationStateEvents>()
                    .AddSingleton<IApplicationLifecycleState>(_ => this)
                    .AddSingleton<INetworkState, XamarinEssentialsNetworkState>()
                    .AddSingleton<ApplicationStateMachine>()
                    .AddSingleton<NetworkStateMachine>()
                    .AddTransient(typeof(IApplicationStateHandler<InitializeApplicationEvent>), typeof(ApplicationStateHandler<InitializeApplicationEvent>))
                    .AddTransient(typeof(IApplicationStateHandler<ResumeApplicationEvent>), typeof(ApplicationStateHandler<ResumeApplicationEvent>))
                    .AddTransient(typeof(IApplicationStateHandler<StartApplicationEvent>), typeof(ApplicationStateHandler<StartApplicationEvent>))
                    .AddTransient(typeof(IApplicationStateHandler<StopApplicationEvent>), typeof(ApplicationStateHandler<StopApplicationEvent>))
                    .AddTransient(typeof(IApplicationStateHandler<GainedSignalEvent>), typeof(ApplicationStateHandler<GainedSignalEvent>))
                    .AddTransient(typeof(IApplicationStateHandler<LostSignalEvent>), typeof(ApplicationStateHandler<LostSignalEvent>))
                    .AddTransient<IMediator, MediatR.Mediator>()
                    .AddTransient<IApplicationStateMediator, ApplicationStateMediator>()
                    .AddMediatR(configuration => configuration.Using<ApplicationStateMediator>(), typeof(ApplicationStateHandler<>))
                    .AddTransient<MainViewModel>()
                    .AddLogging();

            var viewModel =
                (MainViewModel) services.BuildServiceProvider().GetService(typeof(MainViewModel))!;
            MainPage = new MainPage(viewModel);
        }

        public IDisposable Subscribe(IObserver<LifecycleState> observer) => _lifeCycleSubject.Subscribe(observer);

        protected override void OnStart() =>
            // Handle when your app starts
            _lifeCycleSubject.OnNext(LifecycleState.Starting);

        protected override void OnSleep() =>
            // Handle when your app sleeps
            _lifeCycleSubject.OnNext(LifecycleState.Pausing);

        protected override void OnResume() =>
            // Handle when your app resumes
            _lifeCycleSubject.OnNext(LifecycleState.Resuming);

        private readonly Subject<LifecycleState> _lifeCycleSubject = new Subject<LifecycleState>();
    }
}