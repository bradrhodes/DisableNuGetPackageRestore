using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace RemoveNuGetPackageRestore
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
