using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRenamer
{
    public class Cleaner : ICleaner
    {
        private readonly IFileSearcher searcher;
        private readonly ISettings settings;
        private readonly ILogger logger;

        public Cleaner(ISettings settings, IFileSearcher searcher, ILogger logger )
        {
            this.settings = settings;
            this.searcher = searcher;
            this.logger = logger;
        }

        public void Cleanup(Renaming renaming)
        {
            DeleteDir(Path.GetDirectoryName(renaming.RenamedPath));
        }

        public void Cleanup()
        {

  

  
            //cleanup
            var emptyDirs = searcher.GetEmptyDirs(settings.SourcePath);
            while (emptyDirs.Any())
            {
                foreach (var emptyDir in emptyDirs)
                {
                    DeleteDir(emptyDir);

                }
                emptyDirs = searcher.GetEmptyDirs(settings.SourcePath);
            }


        }

        private void DeleteDir(string path)
        {
            try
            {
                if (IsNotRoot(path))
                {
                    if (Directory.Exists(path))
                    {
                        logger.Info($"Delete empty dir {path}");
                        Directory.Delete(path, true);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(e);
            }


        }

        private bool IsNotRoot(string emptyDir)
        {
            return !(settings.ArchivePath.Equals(emptyDir) || settings.MoviePath.Equals(emptyDir) || settings.SeriesPath.Equals(emptyDir));
        }
    }
}
