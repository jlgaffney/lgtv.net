namespace LgTv.Sample.Maui.ViewModels;

public abstract class ViewModelBase(INavigationService navigationService) : ObservableObject
{
    protected INavigationService Navigation { get; } = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

    public virtual Task<bool> LoadAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(true);
    }
}
