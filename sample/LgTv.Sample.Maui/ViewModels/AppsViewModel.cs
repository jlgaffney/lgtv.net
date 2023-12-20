namespace LgTv.Sample.Maui.ViewModels;

public class AppsViewModel(
    INavigationService navigationService,
    IClientController controller)
    : ItemsWithDetailViewModelBase<Features.Apps.App, string>(navigationService, controller)
{
    protected override Task<IEnumerable<Features.Apps.App>> GetItems(CancellationToken cancellationToken = default)
    {
        return Controller.Client.Apps.GetApps();
    }

    protected override string GetId(Features.Apps.App item)
    {
        return item.Id;
    }

    protected override string GetItemUri(string id)
    {
        return $"apps/app?id={id}";
    }
}
