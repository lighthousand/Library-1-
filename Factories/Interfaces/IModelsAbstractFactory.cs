namespace Library.Factories.Interfaces
{
    public interface IModelsAbstractFactory<T>
    {
        T Create();
    }
}
