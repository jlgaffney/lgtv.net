using System.ComponentModel;
using UriTypeConverter = Microsoft.Maui.Controls.UriTypeConverter;

namespace LgTv.Sample.Maui.Services;

public class CustomUriImageSource : ImageSource, IStreamImageSource
{
    public static readonly BindableProperty UriProperty = BindableProperty.Create(
        nameof(Uri), typeof(Uri), typeof(UriImageSource),
        propertyChanged: (bindable, oldValue, newValue) => ((CustomUriImageSource)bindable).OnUriChanged(),
        validateValue: (bindable, value) => value == null || ((Uri)value).IsAbsoluteUri);

    public override bool IsEmpty => Uri == null;

    [TypeConverter(typeof(UriTypeConverter))]
    public Uri Uri
    {
        get => (Uri)GetValue(UriProperty);
        set => SetValue(UriProperty, value);
    }

    async Task<Stream> IStreamImageSource.GetStreamAsync(CancellationToken userToken)
    {
        if (IsEmpty)
        {
            return null;
        }

        return await GetStreamAsync(Uri, userToken);
    }

    private async Task<Stream> GetStreamAsync(Uri uri, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await DownloadStreamAsync(uri, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<Stream> DownloadStreamAsync(Uri uri, CancellationToken cancellationToken)
    {
        try
        {
            var handler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true
            };

            using var client = new HttpClient(handler);

            var response = await client.GetAsync(uri, cancellationToken).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception)
        {
            return null;
        }
    }

    private void OnUriChanged()
    {
        OnSourceChanged();
    }
}
