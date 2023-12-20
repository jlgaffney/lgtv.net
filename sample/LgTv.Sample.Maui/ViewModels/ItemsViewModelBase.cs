namespace LgTv.Sample.Maui.ViewModels;

public abstract partial class ItemsViewModelBase<TItem>(
        INavigationService navigationService,
        IClientController controller)
    : ConnectedViewModelBase(navigationService, controller)
{
    [ObservableProperty]
    private IReadOnlyList<TItem> _items;

    [ObservableProperty]
    private TItem _selectedItem;

    public override async Task<bool> LoadAsync(CancellationToken cancellationToken = default)
    {
        if (!await base.LoadAsync(cancellationToken))
        {
            return false;
        }

        Items = null;

        Items = (await GetItems(cancellationToken)).ToArray();

        return true;
    }

    protected abstract Task<IEnumerable<TItem>> GetItems(CancellationToken cancellationToken = default);
}
