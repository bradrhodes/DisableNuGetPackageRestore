namespace RemoveNuGetPackageRestore
{
    /// <summary>
    /// Cleans NuGet Package Restore references from solution files
    /// </summary>
    public interface ICleanSolutionFile
    {
        bool Clean(string solutionFilePath);
    }
}