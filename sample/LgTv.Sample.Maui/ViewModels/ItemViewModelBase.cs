namespace LgTv.Sample.Maui.ViewModels;

public abstract partial class ItemViewModelBase<TItem, TId>(
        INavigationService navigationService,
        IClientController controller)
    : ConnectedViewModelBase(navigationService, controller)
{
    [ObservableProperty]
    private TId _id;

    [ObservableProperty]
    private TItem _item;

    public override async Task<bool> LoadAsync(CancellationToken cancellationToken = default)
    {
        if (!await base.LoadAsync(cancellationToken))
        {
            return false;
        }

        Item = default;

        Item = await GetItem(Id, cancellationToken);

        return true;
    }

    protected abstract Task<TItem> GetItem(TId id, CancellationToken cancellationToken = default);
}
