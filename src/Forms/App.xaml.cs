using System;
using System.Reactive.Subjects;
using ApplicationState.Machine;
using ApplicationState.Machine.Application;
using ApplicationState.Machine.Application.Background;
using ApplicationState.Machine.Application.Foreground;
using ApplicationState.Machine.Application.Initialize;
using ApplicationState.Machine.Network;
using ApplicationState.Machine.Network.Offline;
using ApplicationState.Machine.Network.Online;
using ApplicationState.Mediator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace ApplicationState
{
    public partial class App : Application, IApplicationLifecycleState
    {
        public App()
        {
            InitializeComponent();
            var services = new ServiceCollection()
                .AddSingleton<ApplicationStateMonitor>()
                .AddSingleton<IApplicationStateEventGenerator, Machine.ApplicationStateEventGenerator>()
                .AddSingleton<IApplicationLifecycleState>(_ => this)
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