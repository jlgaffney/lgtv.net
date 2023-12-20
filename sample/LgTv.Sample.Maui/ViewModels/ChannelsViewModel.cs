using LgTv.Features.Channels;

namespace LgTv.Sample.Maui.ViewModels;

public partial class ChannelsViewModel(
        INavigationService navigationService,
        IClientController controller)
    : ItemsWithDetailViewModelBase<Channel, string>(navigationService, controller)
{
    protected override Task<IEnumerable<Channel>> GetItems(CancellationToken cancellationToken = default)
    {
        return Controller.Client.Channels.GetChannels();
    }

    protected override string GetId(Channel item)
    {
        return item.Id;
    }

    protected override string GetItemUri(string id)
    {
        return $"channels/channel?id={id}";
    }

    [RelayCommand]
    private async Task ChannelUp()
    {
        await Controller.Client.Channels.ChannelUp();
    }

    [RelayCommand]
    private async Task ChannelDown()
    {
        await Controller.Client.Channels.ChannelDown();
    }
}
