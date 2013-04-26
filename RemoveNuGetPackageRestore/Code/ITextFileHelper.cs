namespace RemoveNuGetPackageRestore.Code
{
    /// <summary>
    /// Just wrap up some of the text file methods
    /// </summary>
    public interface ITextFileHelper
    {
        ITextFileHelper LoadFile(string filename);
        ITextFileHelper RemoveMatch(string regularExpression);
        bool Save();
    }
}