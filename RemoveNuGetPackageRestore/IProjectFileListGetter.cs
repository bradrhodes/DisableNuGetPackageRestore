using System.Collections.Generic;

namespace RemoveNuGetPackageRestore
{
    public interface IProjectFileListGetter
    {
        IEnumerable<string> GetProjectFiles(string solutionFolder);
    }
}