using System;
using Delimon.Win32.IO;

namespace AutoRenamer
{
    class FileMover : IFileMover
    {
        private readonly ILogger logger;

        public FileMover(ILogger logger)
        {
            this.logger = logger;
        }

        public bool Move(string from, string to)
        {
            try
            {
                var folderPath = Path.GetDirectoryName(to);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                File.Move(from, to);
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e);
                return false;
            }
        }
    }
}