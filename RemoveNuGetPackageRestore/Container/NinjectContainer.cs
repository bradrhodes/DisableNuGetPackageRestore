using ContainerFramework;

namespace RemoveNuGetPackageRestore.Container
{
    public class NinjectContainer : IContainer
    {
        public NinjectContainer()
        {
            
        }

        public T Resolve<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}