using LgTv.Features.Channels;

namespace LgTv.Sample.Maui.ViewModels;

[QueryProperty(nameof(Id), "id")]
public partial class ChannelViewModel(
        INavigationService navigationService,
        IClientController controller)
    : ItemViewModelBase<Channel, string>(navigationService, controller)
{
    protected override Task<Channel> GetItem(string id, CancellationToken cancellationToken = default)
    {
        return Controller.Client.Channels.GetChannel(id);
    }

    [RelayCommand]
    private async Task SetChannel()
    {
        await Controller.Client.Channels.SetChannel(Id);
    }
}
