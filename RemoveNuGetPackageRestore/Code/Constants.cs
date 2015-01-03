namespace RemoveNuGetPackageRestore.Code
{
    public static class Constants
    {
        public static class RegularExpressions
        {
            public const string SlnFolder = @"Project\([^\r\n]*?\) = "".nuget"".*?EndProject[\W]";
            public const string ProjEnablePackageRestore = @"\s+<RestorePackages>true</RestorePackages>";
            public const string ProjSlnFolder = @"\s+<Import Project=""\$\(SolutionDir\)\\.nuget\\nuget\.targets"" (Condition=""Exists\('\$\(SolutionDir\)\\.nuget\\nuget\.targets'\)"")?\s*/>";
            public const string ProjBuildImportError = @"\s+<Error Condition=""!Exists\('\$\(SolutionDir\)\\.nuget\\NuGet.targets'\)"" Text=""\$\(\[System.String\]::Format\('\$\(ErrorText\)', '\$\(SolutionDir\)\\.nuget\\NuGet.targets'\)\)"" />";
        }

        public static class FolderNames
        {
            public const string NuGetExecutableFolder = @".nuget";
        }

        public static class FileExtensions
        {
            public const string SolutionFile = ".sln";
            public const string ProjectFile = "*.csproj";
        }
    }
}
