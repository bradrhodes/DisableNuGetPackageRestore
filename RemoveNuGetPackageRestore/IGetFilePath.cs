namespace RemoveNuGetPackageRestore
{
    /// <summary>
    /// Gets the path of a file
    /// </summary>
    public interface IGetFilePath
    {
        string GetPath(string filePath);
    }
}