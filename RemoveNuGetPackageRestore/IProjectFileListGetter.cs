using System.Collections.Generic;

namespace RemoveNuGetPackageRestore
{
    /// <summary>
    /// Gets the list of all the project files
    /// </summary>
    public interface IProjectFileListGetter
    {
        IEnumerable<string> GetProjectFiles(string solutionFolder);
    }
}