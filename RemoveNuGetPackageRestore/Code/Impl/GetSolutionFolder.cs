using System.IO;

namespace RemoveNuGetPackageRestore.Code
{
    class GetSolutionFolder : IGetSolutionFolder
    {
        public string GetFolder(string solutionFilePath)
        {
            return Path.GetDirectoryName(solutionFilePath);
        }
    }
}