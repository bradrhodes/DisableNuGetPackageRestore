using System;
using System.Windows.Forms;
using System.IO;
using RemoveNuGetPackageRestore.Code;
using RemoveNuGetPackageRestore.Container;

namespace RemoveNuGetPackageRestore
{
    public partial class Form1 : Form
    {
        private readonly IGetFilePath _filePathGetter;
        private readonly IRemoveNuGetPackageRestore _packageRestoreRemover;

        public Form1(IIOCContainer container) : this()
        {
            _filePathGetter = container.Resolve<IGetFilePath>();
            _packageRestoreRemover = container.Resolve<IRemoveNuGetPackageRestore>();
        }

        private Form1()
        {
            InitializeComponent();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            var initialDirectory = _filePathGetter.GetPath(textFolder.Text);

            var fld = new OpenFileDialog()
                {
                    Filter = "Visual Studio Solution files (*.sln)|*.sln",
                    AutoUpgradeEnabled = true,
                    InitialDirectory = initialDirectory
                };

            if (fld.ShowDialog() == DialogResult.OK)
            {
                textFolder.Text = fld.FileName;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            // Check to make sure a valid path exists and that it's a solution file
            var filePath = _filePathGetter.GetPath(textFolder.Text);
            if (String.IsNullOrEmpty(filePath) || Path.GetExtension(filePath) != Constants.FileExtensions.SolutionFile)
            {
                MessageBox.Show("Please select a valid Visual Studio Solution.", "Error", MessageBoxButtons.OK);
                return;
            }

            // Pop confirmation box
            if (MessageBox.Show("Please ensure the solution is not open in Visual Studio before proceeding.", "Proceed?",
                                MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;

            if (_packageRestoreRemover.RemovePackageRestore(filePath))
                PopSuccess();
            else
                PopFail();
        }

        private void PopSuccess()
        {
            MessageBox.Show("Successfully removed the NuGet Package Restore");
        }

        private void PopFail()
        {
            MessageBox.Show("Something went wrong. Some references for NuGet Package Restore may not have been removed.");
        }
    }
}
