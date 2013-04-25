using System.Collections.Generic;
using System.IO;

namespace RemoveNuGetPackageRestore
{
    class ProjectFileListGetter : IProjectFileListGetter
    {
        public IEnumerable<string> GetProjectFiles(string solutionFolder)
        {
            return Directory.GetFiles(solutionFolder, Constants.FileExtensions.ProjectFile, SearchOption.AllDirectories);
        }
    }
}