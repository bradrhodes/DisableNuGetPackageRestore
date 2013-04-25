namespace RemoveNuGetPackageRestore
{
    public interface ICleanProjectFiles
    {
        bool Clean(string solutionFolder);
    }
}