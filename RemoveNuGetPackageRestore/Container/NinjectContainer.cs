using Ninject;
using RemoveNuGetPackageRestore.Code;

namespace RemoveNuGetPackageRestore.Container
{
    class NinjectContainer : IIOCContainer
    {
        private readonly IKernel _ninjectContainer;
        
        public NinjectContainer()
        {
            _ninjectContainer = new StandardKernel();
            SetupContainer();
        }

        public T Resolve<T>()
        {
            return _ninjectContainer.Get<T>();
        }

        private void SetupContainer()
        {
            _ninjectContainer.Bind<IGetFilePath>().To<GetFilePath>();
            _ninjectContainer.Bind<IRemoveNuGetPackageRestore>().To<RemoveNuGetPackageRestoreFacade>();
            _ninjectContainer.Bind<IRemoveNuGetFolder>().To<RemoveNuGetFolder>();
            _ninjectContainer.Bind<IGetSolutionFolder>().To<GetSolutionFolder>();
            _ninjectContainer.Bind<ICleanSolutionFile>().To<CleanSolutionFile>();
            _ninjectContainer.Bind<ITextFileHelper>().To<TextFileHelper>();
            _ninjectContainer.Bind<ICleanProjectFiles>().To<CleanProjectFiles>();
            _ninjectContainer.Bind<IProjectFileListGetter>().To<ProjectFileListGetter>();
        }
    }
}