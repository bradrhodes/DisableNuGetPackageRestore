using System.IO;

namespace RemoveNuGetPackageRestore
{
    class GetSolutionFolder : IGetSolutionFolder
    {
        public string GetFolder(string solutionFilePath)
        {
            return Path.GetDirectoryName(solutionFilePath);
        }
    }
}