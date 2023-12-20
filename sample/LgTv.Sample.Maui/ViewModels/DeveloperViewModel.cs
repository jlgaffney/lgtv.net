using Newtonsoft.Json;

namespace LgTv.Sample.Maui.ViewModels;

public partial class DeveloperViewModel(
        INavigationService navigationService,
        IClientController controller)
    : ConnectedViewModelBase(navigationService, controller)
{
    private const string DefaultPayloadJson = @"{
  
}";

    [ObservableProperty]
    private string _prefix;

    [ObservableProperty]
    private string _uri;

    [ObservableProperty]
    private string _payloadJson = DefaultPayloadJson;

    [ObservableProperty]
    private string _responseJson;

    [RelayCommand]
    private async Task SendCommand()
    {
        if (string.IsNullOrWhiteSpace(Uri))
        {
            return;
        }

        var payload = string.IsNullOrWhiteSpace(PayloadJson) ? DefaultPayloadJson : PayloadJson;

        RequestMessage request;
        if (!string.IsNullOrWhiteSpace(Prefix))
        {
            request = new RequestMessage(Prefix, Uri);
            request.SetPayload(payload);
        }
        else
        {
            request = new RequestMessage(Uri, (object)payload);
        }

        var response = await Controller.Client.SendCommand(request);

        ResponseJson = JsonConvert.SerializeObject(response, Formatting.Indented);
    }
}
