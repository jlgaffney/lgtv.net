using LgTv.Features.Inputs;

namespace LgTv.Sample.Maui.ViewModels;

[QueryProperty(nameof(Id), "id")]
public partial class InputViewModel(
        INavigationService navigationService,
        IClientController controller)
    : ItemViewModelBase<Input, string>(navigationService, controller)
{
    protected override Task<Input> GetItem(string id, CancellationToken cancellationToken = default)
    {
        return Controller.Client.Inputs.GetInput(id);
    }

    [RelayCommand]
    private async Task SetInput()
    {
        await Controller.Client.Inputs.SetInput(Id);
    }
}
