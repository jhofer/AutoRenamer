using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRenamer
{
    class FileSearcher : IFileSearcher
    {
        private ISettings settings;

        public FileSearcher(ISettings settings)
        {
            this.settings = settings;
        }

        public List<string> GetEmptyDirs(string root)
        {
            return EmptyDirs(root, true);
        }

        private List<string> EmptyDirs(string root, bool isFirst)
        {
            List<string> emptyDirs = new List<string>();
            if (Directory.GetFiles(root).Any() || Directory.GetDirectories(root).Any())
            {
                List<string> alldirs = Directory.GetDirectories(root).ToList();
                foreach (var dir in alldirs)
                {
                    emptyDirs.AddRange(EmptyDirs(dir,false));
                }
            }
            else if (!isFirst) 
            {
                emptyDirs.Add(root);
            }

            return emptyDirs;
        }

        public List<string> GetFilePaths(string root, List<string> extensions)
        {
            List<string> filePaths = new List<string>();
            if (IsNoDropFolder(root))
            {

                var directories = Directory.GetDirectories(root);
                foreach (var extension in extensions)
                {
                    filePaths.AddRange(Directory.GetFiles(root, extension));
                }
                foreach (var dir in directories)
                {
                    filePaths.AddRange(GetFilePaths(dir, extensions));
                }
            }
            return filePaths;
        }

        private bool IsNoDropFolder(string root)
        {
            return !root.Equals(settings.ArchivePath) && !root.Equals(settings.MoviePath) && !root.Equals(settings.SeriesPath);
        }
    }
}
