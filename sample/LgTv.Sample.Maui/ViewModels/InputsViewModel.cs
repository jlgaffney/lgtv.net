using LgTv.Features.Inputs;

namespace LgTv.Sample.Maui.ViewModels;

public class InputsViewModel(
        INavigationService navigationService,
        IClientController controller)
    : ItemsWithDetailViewModelBase<Input, string>(navigationService, controller)
{
    protected override Task<IEnumerable<Input>> GetItems(CancellationToken cancellationToken = default)
    {
        return Controller.Client.Inputs.GetInputs();
    }

    protected override string GetId(Input item)
    {
        return item.Id;
    }

    protected override string GetItemUri(string id)
    {
        return $"inputs/input?id={id}";
    }
}
