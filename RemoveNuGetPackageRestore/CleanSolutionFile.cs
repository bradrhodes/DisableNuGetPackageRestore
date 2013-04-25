using System;

namespace RemoveNuGetPackageRestore
{
    class CleanSolutionFile : ICleanSolutionFile
    {
        private readonly ITextFileHelper _textFileHelper;

        public CleanSolutionFile(ITextFileHelper textFileHelper)
        {
            if (textFileHelper == null) throw new ArgumentNullException("textFileHelper");
            _textFileHelper = textFileHelper;
        }

        public bool Clean(string solutionFilePath)
        {
            return _textFileHelper.LoadFile(solutionFilePath).RemoveMatch(Constants.RegularExpressions.SlnFolder).Save();
        }
    }
}