using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace RemoveNuGetPackageRestore
{
    /// <summary>
    /// This is a facade to do all the necessary work
    /// </summary>
    public interface IRemoveNuGetPackageRestore
    {
        bool RemovePackageRestore(string solutionFilePath);
    }

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

    public interface ICleanProjectFiles
    {
        bool Clean(string solutionFolder);
    }

    class CleanProjectFiles : ICleanProjectFiles
    {
        private readonly IXmlHelper _xmlHelper;
        private readonly IProjectFileListGetter _projectFileListGetter;

        public CleanProjectFiles(IXmlHelper xmlHelper, IProjectFileListGetter projectFileListGetter)
        {
            if (xmlHelper == null) throw new ArgumentNullException("xmlHelper");
            if (projectFileListGetter == null) throw new ArgumentNullException("projectFileListGetter");
            _xmlHelper = xmlHelper;
            _projectFileListGetter = projectFileListGetter;
        }

        public bool Clean(string solutionFolder)
        {
            var fileList = _projectFileListGetter.GetProjectFiles(solutionFolder);

            var allClean = true;

            foreach (var filePath in fileList)
            {
                allClean &= _xmlHelper.LoadXmlFile(filePath)
                          .RemoveNode(@"//Import[@Project='$(SolutionDir)\.nuget\nuget.targets']")
                          .SaveFile();

                allClean &= _xmlHelper.LoadXmlFile(filePath)
                        .RemoveNode("//RestorePackages")
                        .SaveFile();
            }

            return allClean;
        }
    }

    public interface ITextFileHelper
    {
        ITextFileHelper LoadFile(string filename);
        ITextFileHelper RemoveMatch(string regularExpression);
        bool Save();
    }

    class TextFileHelper : ITextFileHelper
    {
        private string _filename;
        private string _fileContents;

        public ITextFileHelper LoadFile(string filename)
        {
            _filename = filename;

            // Read the file contents
            var sr = new StreamReader(File.Open(_filename, FileMode.Open));
            _fileContents = sr.ReadToEnd();
            sr.Close();

            return this;
        }

        public ITextFileHelper RemoveMatch(string pattern)
        {
            if(String.IsNullOrEmpty(_fileContents))
                throw new ArgumentException("Text file must be loaded and parsed first.");

            var regex = new Regex(pattern, RegexOptions.Singleline);

            var result = regex.Replace(_fileContents, "");

            _fileContents = result;

            return this;
        }

        public bool Save()
        {
            try
            {
                var sw = new StreamWriter(File.Open(_filename, FileMode.Create));

                sw.Write(_fileContents);
                sw.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public interface IProjectFileListGetter
    {
        IEnumerable<string> GetProjectFiles(string solutionFolder);
    }

    class ProjectFileListGetter : IProjectFileListGetter
    {
        public IEnumerable<string> GetProjectFiles(string solutionFolder)
        {
            return Directory.GetFiles(solutionFolder, Constants.FileExtensions.ProjectFile, SearchOption.AllDirectories);
        }
    }


    public interface ICleanSolutionFile
    {
        bool Clean(string solutionFilePath);
    }

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

    public interface IXmlHelper
    {
        IXmlHelper LoadXmlFile(string xmlFileName);
        IXmlHelper RemoveNode(string xpath);
        bool SaveFile();
    }

    class XmlHelper : IXmlHelper
    {
        private XmlDocument _xmlDoc;
        private string _xmlFileName;

        public IXmlHelper LoadXmlFile(string xmlFileName)
        {
            _xmlFileName = xmlFileName;
            _xmlDoc = new XmlDocument();
            _xmlDoc.Load(xmlFileName);

            return this;
        }

        public IXmlHelper RemoveNode(string xpath)
        {
            if(_xmlDoc == null)
                throw new ArgumentException("XmlFile must be loaded first.");

            var nodes = _xmlDoc.SelectNodes(xpath);

            if (nodes == null || nodes.Count == 0)
                return this;

            foreach (XmlNode node in nodes)
            {
                _xmlDoc.RemoveChild(node);    
            }

            return this;
        }

        public bool SaveFile()
        {
            try
            {
                _xmlDoc.Save(_xmlFileName);
                _xmlDoc = null;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public interface IGetSolutionFolder
    {
        string GetFolder(string solutionFilePath);
    }

    class GetSolutionFolder : IGetSolutionFolder
    {
        public string GetFolder(string solutionFilePath)
        {
            return Path.GetDirectoryName(solutionFilePath);
        }
    }

    public interface IRemoveNuGetFolder
    {
        bool RemoveFolder(string folder);
    }

    class RemoveNuGetFolder : IRemoveNuGetFolder
    {
        public bool RemoveFolder(string folder)
        {
            var nuGetFolder = String.Format(@"{0}\{1}", folder, Constants.FolderNames.NuGetExecutableFolder);

            if (!Directory.Exists(nuGetFolder))
                return true;

            try
            {
                Directory.Delete(nuGetFolder, true);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
