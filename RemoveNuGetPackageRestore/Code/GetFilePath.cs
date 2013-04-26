using System.IO;

namespace RemoveNuGetPackageRestore.Code
{
    class GetFilePath : IGetFilePath
    {
        public string GetPath(string filePath)
        {
            try
            {
                return Path.GetFullPath(filePath);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}