namespace LgTv.Sample.Maui.Views;

public class ApplicationPage : ContentPage
{
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (BindingContext is ViewModelBase viewModel)
        {
            await viewModel.LoadAsync();
        }
    }
}
