using System.ComponentModel;

namespace LgTv.Sample.Maui.ViewModels;

public abstract partial class ItemsWithDetailViewModelBase<TItem, TId>(
        INavigationService navigationService,
        IClientController controller)
    : ItemsViewModelBase<TItem>(navigationService, controller)
{
    protected abstract TId GetId(TItem item);

    protected abstract string GetItemUri(TId id);

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.PropertyName == nameof(SelectedItem) && SelectedItem != null)
        {
            NavigateToItemCommand.Execute(SelectedItem);
        }
    }

    [RelayCommand]
    private async Task NavigateToItem(TItem item)
    {
        if (item == null)
        {
            return;
        }

        await Navigation.NavigateAsync(GetItemUri(GetId(item)));
    }
}
