using System;
using System.Collections.Generic;
using System.Linq;
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

        private readonly List<string> irgnoredMoviePaths = new List<string>(); 
        private readonly List<string> irgnoredSeriesPaths = new List<string>(); 

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
            try
            {
                logger.Info("Start Renaming...");
                var filePaths = fileSearcher.GetFilePaths(settings.SourcePath, settings.Extensions);
                var renamings = new List<Renaming>();
                cleaner.Cleanup();
                Rename(filePaths, movieFileBot, renamings, settings.MoviePath, this.irgnoredMoviePaths);
                LogRenaming(renamings);
                cleaner.Cleanup();

                Rename(filePaths, seriesFileBot, renamings, settings.SeriesPath, this.irgnoredSeriesPaths);
                LogRenaming(renamings);
                cleaner.Cleanup();


                LogRenaming(renamings);
                logger.Info("Renaming done...");
            }
            catch (Exception e)
            {
                logger.Error(e);
            }
        }

        private void LogRenaming(List<Renaming> renamings)
        {
            if (renamings.Any())
            {
                StringBuilder builder = new StringBuilder();
                foreach (var renaming in renamings)
                {
                    if (renaming != null)
                    {
                        builder.Append($"{renaming} {Environment.NewLine}");
                    }
                 
                }
                logger.Info(builder.ToString());
            }
           
        }

        private void Rename(List<string> filePaths, IFileBot fileBot,
            List<Renaming> renamings, string dropfolder, List<string> irgnoredPaths )
        {
            foreach (var filePath in filePaths)
            {
                var newFileName = String.Empty;
                if (!irgnoredPaths.Contains(filePath))
                {
                    if (fileBot.Rename(filePath, out newFileName))
                    {
                        var renaming = pathGenerator.GenerateNewPath(filePath, newFileName, dropfolder);
                        archiver.ArchiveExisting(renaming.NewPath);
                        if (fileMover.Move(renaming.RenamedPath, renaming.NewPath))
                        {
                            renamings.Add(renaming);
                            cleaner.Cleanup(renaming);
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