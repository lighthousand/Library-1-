using Library.Factories.Interfaces;

namespace Library.Factories
{
    public class ModelsAbstractFactory<T> : IModelsAbstractFactory<T>
    {
        private readonly Func<T> _factory;

        public ModelsAbstractFactory(Func<T> factory)
        {
            _factory = factory;
        }

        public T Create()
        {
            return _factory();
        }
    }

    public static class ViewModelsAbstractFactoryExtension
    {
        public static void AddModelsAbstractFactory<TInterface, TClass>(this IServiceCollection services)
            where TInterface : class
            where TClass : class, TInterface
        {
            services.AddTransient<TInterface, TClass>();
            services.AddSingleton<Func<TInterface>>(x => () => x.GetService<TInterface>());
            services.AddSingleton<IModelsAbstractFactory<TInterface>, ModelsAbstractFactory<TInterface>>();
        }
    }
}
