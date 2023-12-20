namespace LgTv.Sample.Maui.Services;

public static class ServiceLocator
{
    private static IServiceProvider _current;

    public static T Get<T>() => _current.GetService<T>();

    public static object Get(Type type) => _current.GetService(type);

    public static void Initialize(IServiceProvider serviceProvider)
    {
        _current = serviceProvider;
    }
}
