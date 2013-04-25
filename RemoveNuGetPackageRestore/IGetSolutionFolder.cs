namespace RemoveNuGetPackageRestore
{
    public interface IGetSolutionFolder
    {
        string GetFolder(string solutionFilePath);
    }
}