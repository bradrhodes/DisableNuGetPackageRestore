namespace RemoveNuGetPackageRestore
{
    /// <summary>
    /// This is a facade to do all the necessary work
    /// </summary>
    public interface IRemoveNuGetPackageRestore
    {
        bool RemovePackageRestore(string solutionFilePath);
    }
}