namespace RemoveNuGetPackageRestore.Code
{
    /// <summary>
    /// Gets the folder of the solution file
    /// </summary>
    public interface IGetSolutionFolder
    {
        string GetFolder(string solutionFilePath);
    }
}