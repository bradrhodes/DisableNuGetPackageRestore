namespace RemoveNuGetPackageRestore
{
    public interface ICleanSolutionFile
    {
        bool Clean(string solutionFilePath);
    }
}