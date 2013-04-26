using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using RemoveNuGetPackageRestore.Code;

namespace RemoveNuGetPackageRestore
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(
                new Form1(
                    new GetFilePath(), 
                    new Code.RemoveNuGetPackageRestore(
                        new RemoveNuGetFolder(), 
                        new GetSolutionFolder(), 
                        new CleanSolutionFile(
                            new TextFileHelper()),
                        new CleanProjectFiles(
                            new TextFileHelper(), 
                            new ProjectFileListGetter())))
                );
        }
    }
}
