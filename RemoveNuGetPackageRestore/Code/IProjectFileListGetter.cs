using System.Collections.Generic;

namespace RemoveNuGetPackageRestore.Code
{
    /// <summary>
    /// Gets the list of all the project files
    /// </summary>
    public interface IProjectFileListGetter
    {
        IEnumerable<string> GetProjectFiles(string solutionFolder);
    }
}