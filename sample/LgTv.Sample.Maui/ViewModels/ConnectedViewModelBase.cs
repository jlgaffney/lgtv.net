namespace LgTv.Sample.Maui.ViewModels;

/// <summary>
/// Base view model class for view models that require a TV connection.
/// </summary>
public abstract class ConnectedViewModelBase : ViewModelBase
{
    protected ConnectedViewModelBase(
        INavigationService navigationService,
        IClientController controller)
        : base(navigationService)
    {
        Controller = controller ?? throw new ArgumentNullException(nameof(controller));

        Task.Run(RedirectIfNotConnected);
    }

    internal IClientController Controller { get; }

    public override async Task<bool> LoadAsync(CancellationToken cancellationToken = default)
    {
        if (await RedirectIfNotConnected())
        {
            return false;
        }

        return await base.LoadAsync(cancellationToken);
    }

    /// <summary>
     /// Redirects to the connection page if a TV is not connected.
     /// </summary>
     /// <returns><see langword="true" /> if redirected, otherwise <see langword="false"/>.</returns>
    protected async Task<bool> RedirectIfNotConnected()
    {
        if (Controller.IsConnected)
        {
            return false;
        }

        await Navigation.NavigateAsync("connect");

        return true;
    }
}
