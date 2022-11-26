using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ApplicationState.Machine;
using ApplicationState.Machine.Events;
using ApplicationState.Mediator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Unit = System.Reactive.Unit;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace ApplicationState
{
    public partial class App : Application, IApplicationTriggers
    {
        public App()
        {
            InitializeComponent();
            var services = new ServiceCollection();

            services
                .AddSingleton<ApplicationStateMonitor>()
                .AddSingleton<IApplicationEvents, ApplicationEvents>()
                .AddSingleton<IApplicationTriggers>(_ => this)
                .AddSingleton<INetworkState, NetworkState>()
                .AddSingleton<ApplicationStatelessMachine>()
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

        public IObservable<Unit> Initializing => _initialize.AsObservable();
        public IObservable<Unit> Starting => _start.AsObservable();
        public IObservable<Unit> Pausing => _stop.AsObservable();
        public IObservable<Unit> Resuming => _start.AsObservable().Skip(1);

        protected override void OnStart()
        {
            // Handle when your app starts
            _initialize.OnNext(Unit.Default);
            _start.OnNext(Unit.Default);
        }

        protected override void OnSleep() =>
            // Handle when your app sleeps
            _stop.OnNext(Unit.Default);

        protected override void OnResume() =>
            // Handle when your app resumes
            _start.OnNext(Unit.Default);

        private readonly AsyncSubject<Unit> _initialize = new AsyncSubject<Unit>();
        private readonly Subject<Unit> _start = new Subject<Unit>();
        private readonly Subject<Unit> _stop = new Subject<Unit>();
    }

    public class NetworkState : INetworkState
    {
        public IDisposable Subscribe(IObserver<NetworkStateChangedEvent> observer)
        {
            return Disposable.Empty;
        }
    }
}