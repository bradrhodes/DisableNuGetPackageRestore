namespace RemoveNuGetPackageRestore
{
    public interface ITextFileHelper
    {
        ITextFileHelper LoadFile(string filename);
        ITextFileHelper RemoveMatch(string regularExpression);
        bool Save();
    }
}