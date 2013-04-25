using System;
using System.IO;
using System.Text.RegularExpressions;

namespace RemoveNuGetPackageRestore
{
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
}