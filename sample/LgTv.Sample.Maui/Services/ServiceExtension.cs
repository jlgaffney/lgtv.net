namespace LgTv.Sample.Maui.Services;

[ContentProperty(nameof(TypeName))]
public class ServiceExtension : IMarkupExtension
{
    public string TypeName { get; set; }

    public object ProvideValue(IServiceProvider serviceProvider)
    {
        if (serviceProvider == null)
        {
            throw new ArgumentNullException(nameof(serviceProvider));
        }

        var typeResolver = serviceProvider.GetService<IXamlTypeResolver>();
        if (typeResolver == null)
        {
            throw new ArgumentException("No IXamlTypeResolver in IServiceProvider");
        }

        var lineInfoProvider = serviceProvider.GetService<IXmlLineInfoProvider>();
        var lineInfo = lineInfoProvider?.XmlLineInfo ?? new XmlLineInfo();

        if (string.IsNullOrEmpty(TypeName))
        {
            throw new XamlParseException($"{nameof(TypeName)} is not set.", lineInfo);
        }

        if (!typeResolver.TryResolve(TypeName, out var type))
        {
            throw new XamlParseException($"ServiceExtension: Could not locate type for {TypeName}.", lineInfo);
        }

        var service = ServiceLocator.Get(type);
        if (service == null)
        {
            throw new XamlParseException($"ServiceExtension: Could not locate service for {type.Name}.", lineInfo);
        }

        return service;
    }
}
