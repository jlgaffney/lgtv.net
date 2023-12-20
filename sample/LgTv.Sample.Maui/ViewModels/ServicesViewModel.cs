using LgTv.Features.Info;

namespace LgTv.Sample.Maui.ViewModels;

public class ServicesViewModel(
        INavigationService navigationService,
        IClientController controller)
    : ItemsViewModelBase<Service>(navigationService, controller)
{
    protected override Task<IEnumerable<Service>> GetItems(CancellationToken cancellationToken = default)
    {
        return Controller.Client.Info.GetServices();
    }
}
