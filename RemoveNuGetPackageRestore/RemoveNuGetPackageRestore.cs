using System;

namespace RemoveNuGetPackageRestore
{
    public class RemoveNuGetPackageRestore : IRemoveNuGetPackageRestore
    {
        private readonly IRemoveNuGetFolder _folderRemover;
        private readonly IGetSolutionFolder _solutionFolderGetter;
        private readonly ICleanSolutionFile _solutionFileCleaner;
        private readonly ICleanProjectFiles _projectFileCleaner;

        public RemoveNuGetPackageRestore(IRemoveNuGetFolder folderRemover, IGetSolutionFolder solutionFolderGetter, 
                                         ICleanSolutionFile solutionFileCleaner, ICleanProjectFiles projectFileCleaner)
        {
            if (folderRemover == null) throw new ArgumentNullException("folderRemover");
            if (solutionFolderGetter == null) throw new ArgumentNullException("solutionFolderGetter");
            if (solutionFileCleaner == null) throw new ArgumentNullException("solutionFileCleaner");
            if (projectFileCleaner == null) throw new ArgumentNullException("projectFileCleaner");
            _folderRemover = folderRemover;
            _solutionFolderGetter = solutionFolderGetter;
            _solutionFileCleaner = solutionFileCleaner;
            _projectFileCleaner = projectFileCleaner;
        }

        public bool RemovePackageRestore(string solutionFilePath)
        {
            var result = true;

            // Get the folder for solution file
            var solutionPath = _solutionFolderGetter.GetFolder(solutionFilePath);

            // Remove the NuGetFolder
            result &= _folderRemover.RemoveFolder(solutionPath);

            // Clean the solution file
            result &= _solutionFileCleaner.Clean(solutionFilePath);

            // Clean all project files
            result &= _projectFileCleaner.Clean(solutionPath);

            return result;
        }
    }
}