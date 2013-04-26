namespace RemoveNuGetPackageRestore.Code
{
    /// <summary>
    /// Removes the .nuget folder
    /// </summary>
    public interface IRemoveNuGetFolder
    {
        bool RemoveFolder(string folder);
    }
}