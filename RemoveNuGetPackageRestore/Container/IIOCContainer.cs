namespace RemoveNuGetPackageRestore.Container
{
    public interface IIOCContainer
    {
        T Resolve<T>();
    }
}
