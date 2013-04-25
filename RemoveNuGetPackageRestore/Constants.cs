using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoveNuGetPackageRestore
{
    public static class Constants
    {
        public static class RegularExpressions
        {
            public const string SlnFolder = @"Project\([^\r\n]*?\) = "".nuget"".*?EndProject[\W]";
            public const string ProjEnablePackageRestore = @"\s+<RestorePackages>true</RestorePackages>";
            public const string ProjSlnFolder = @"\s+<Import Project=""\$\(SolutionDir\)\\.nuget\\nuget\.targets"" />";
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
