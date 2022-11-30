using System.Reactive;
using System.Reactive.Disposables;
using ReactiveUI;

namespace ApplicationState
{
    public partial class MainPage
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

            this.WhenActivated(disposables => Interactions
                .UnhandledTransitions
                .RegisterHandler(context =>
                    DisplayAlert("Unhandled Transition", context.Input, "Okay")
                        .ContinueWith(task =>
                        {
                            if (task.IsCompletedSuccessfully)
                            {
                                context.SetOutput(Unit.Default);
                            }
                        }))
                .DisposeWith(disposables));
        }
    }
}