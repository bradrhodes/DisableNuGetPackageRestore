namespace RemoveNuGetPackageRestore
{
    /// <summary>
    /// Cleans NuGet Package Restore references from project files
    /// </summary>
    public interface ICleanProjectFiles
    {
        bool Clean(string solutionFolder);
    }
}