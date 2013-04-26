using System;
using System.IO;

namespace RemoveNuGetPackageRestore.Code
{
    class RemoveNuGetFolder : IRemoveNuGetFolder
    {
        public bool RemoveFolder(string folder)
        {
            var nuGetFolder = String.Format(@"{0}\{1}", folder, Constants.FolderNames.NuGetExecutableFolder);

            if (!Directory.Exists(nuGetFolder))
                return true;

            try
            {
                Directory.Delete(nuGetFolder, true);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
