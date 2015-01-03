using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using RemoveNuGetPackageRestore.Code;

namespace Tests
{
    [TestClass]
    public class ProjRegExTests
    {

        [TestMethod]
        public void Should_Remove_Restore_Packages()
        {
            var regex = BuildExpression(Constants.RegularExpressions.ProjEnablePackageRestore);

            var result = regex.Replace(GetTestString(), "");

            Assert.IsTrue(!result.Contains("RestorePackages"), "Restore Packages was not removed");
        }

        [TestMethod]
        public void Should_Remove_Project_Import()
        {
            var regex = BuildExpression(Constants.RegularExpressions.ProjSlnFolder);

            var result = regex.Replace(GetTestString(), "");

            Assert.IsTrue(!result.Contains("Import"), "Project Import was not removed");
        }

        [TestMethod]
        public void Should_Remove_Project_Build_Import_Error()
        {
            var regex = BuildExpression(Constants.RegularExpressions.ProjBuildImportError);

            var result = regex.Replace(GetTestString(), "");

            Assert.IsTrue(!result.Contains("Error"), "Project  Build Import Error was not removed");
        }

        private Regex BuildExpression(string pattern)
        {
            return new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
        }

        private static string GetTestString()
        {
            return
@"
  <RestorePackages>true</RestorePackages>
  <Import Project=""$(SolutionDir)\.nuget\NuGet.targets"" />
  <Import Project=""$(SolutionDir)\.nuget\NuGet.targets"" Condition=""Exists('$(SolutionDir)\.nuget\NuGet.targets')"" />
  <Error Condition=""!Exists('$(SolutionDir)\.nuget\NuGet.targets')"" Text=""$([System.String]::Format('$(ErrorText)', '$(SolutionDir)\.nuget\NuGet.targets'))"" />
";
        }
    }
}
