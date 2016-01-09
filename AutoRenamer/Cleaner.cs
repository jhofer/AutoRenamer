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

        public void Cleanup()
        {
            //cleanup
            var emptyDirs = searcher.GetEmptyDirs(settings.SourcePath);
            while (emptyDirs.Any())
            {
                foreach (var emptyDir in emptyDirs)
                {
                    try
                    {

                        if (IsRoot(emptyDir))
                        {
                            logger.Info($"Delete empty dir {emptyDir}");
                            Directory.Delete(emptyDir);
                        }

                    }
                    catch (Exception e)
                    {
                        logger.Error(e);
                    }

                }
                emptyDirs = searcher.GetEmptyDirs(settings.SourcePath);
            }
        }

        private bool IsRoot(string emptyDir)
        {
            return !(settings.ArchivePath.Equals(emptyDir) || settings.MoviePath.Equals(emptyDir) || settings.SeriesPath.Equals(emptyDir));
        }
    }
}
