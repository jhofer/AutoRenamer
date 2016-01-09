using System.Collections.Generic;
using System.Text;

namespace AutoRenamer
{
    public class Renamer
    {
        private readonly IArchiver archiver;
        private readonly ICleaner cleaner;
        private readonly IFileMover fileMover;
        private readonly IFileSearcher fileSearcher;
        private readonly ILogger logger;
        private readonly IFileBot movieFileBot;
        private readonly IPathGenerator pathGenerator;
        private readonly IFileBot seriesFileBot;
        private readonly ISettings settings;

        private List<string> irgnoredPaths = new List<string>(); 

        public Renamer()
        {
            this.logger = new Logger();
            this.settings = new Settings(logger);
            this.fileSearcher = new FileSearcher(settings, logger);
            this.movieFileBot = new FileBot(settings, logger, true);
            this.seriesFileBot = new FileBot(settings, logger, false);
            this.cleaner = new Cleaner(settings, fileSearcher, logger);
            this.pathGenerator = new PathGenerator();
            this.fileMover = new FileMover(logger);
            this.cleaner = new Cleaner(settings, fileSearcher, logger);
            this.archiver = new Archiver(settings, fileMover);
        }

        public void Run()
        {
            logger.Info("Start Renaming...");
            var filePaths = fileSearcher.GetFilePaths(settings.SourcePath, settings.Extensions);
            var renamings = new List<Renaming>();
            Rename(filePaths, movieFileBot, renamings, settings.MoviePath);
            cleaner.Cleanup();
            Rename(filePaths, seriesFileBot, renamings, settings.SeriesPath);
            cleaner.Cleanup();

            StringBuilder builder = new StringBuilder();
            foreach (var renaming in renamings)
            {
                builder.Append($"{renaming} \n");
            }
            logger.Info(builder.ToString());
            logger.Info("Renaming done...");
        }

        private void Rename(List<string> filePaths, IFileBot fileBot,
            List<Renaming> renamings, string dropfolder)
        {
            foreach (var filePath in filePaths)
            {
                var newFileName = "";
                if (!irgnoredPaths.Contains(filePath))
                {
                    if (fileBot.Rename(filePath, out newFileName))
                    {
                        var renaming = pathGenerator.GenerateNewPath(filePath, newFileName, dropfolder);
                        archiver.ArchiveExisting(renaming.NewPath);
                        if (fileMover.Move(renaming.RenamedPath, renaming.NewPath))
                        {
                            renamings.Add(renaming);
                        }
                    }
                    else
                    {
                        irgnoredPaths.Add(filePath);
                    }
                }
               
            }
        }
    }
}