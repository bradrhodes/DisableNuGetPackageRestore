using System;
using RemoveNuGetPackageRestore.Code;

namespace RemoveNuGetPackageRestore.Container
{
    public class DefaultContainer : IIOCContainer
    {
        public T Resolve<T>()
        {
            var type = typeof (T);

            return (T) CreateObject(type);
        }

        private object CreateObject(Type objectType)
        {
            if(objectType == typeof(IGetFilePath))
                return new GetFilePath();

            if (objectType == typeof (IRemoveNuGetPackageRestore))
                return new RemoveNuGetPackageRestoreFacade(
                    new RemoveNuGetFolder(),
                    new GetSolutionFolder(),
                    new CleanSolutionFile(
                        new TextFileHelper()),
                    new CleanProjectFiles(
                        new TextFileHelper(),
                        new ProjectFileListGetter()));

            // Perform some default action
            throw new ArgumentException("DefaultContainer cannot create an instance of the required type");
        }
    }
}