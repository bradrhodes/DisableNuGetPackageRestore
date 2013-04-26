using System;

namespace RemoveNuGetPackageRestore.Code
{
    class CleanProjectFiles : ICleanProjectFiles
    {
        private readonly ITextFileHelper _textFileHelper;
        private readonly IProjectFileListGetter _projectFileListGetter;

        public CleanProjectFiles(ITextFileHelper textFileHelper, IProjectFileListGetter projectFileListGetter)
        {
            if (textFileHelper == null) throw new ArgumentNullException("textFileHelper");
            if (projectFileListGetter == null) throw new ArgumentNullException("projectFileListGetter");
            _textFileHelper = textFileHelper;
            _projectFileListGetter = projectFileListGetter;
        }

        public bool Clean(string solutionFolder)
        {
            var fileList = _projectFileListGetter.GetProjectFiles(solutionFolder);

            var allClean = true;

            foreach (var filePath in fileList)
            {
                allClean &=
                    _textFileHelper.LoadFile(filePath)
                                   .RemoveMatch(Constants.RegularExpressions.ProjEnablePackageRestore)
                                   .RemoveMatch(Constants.RegularExpressions.ProjSlnFolder).Save();
            }

            return allClean;
        }
    }
}