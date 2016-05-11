using System;
using System.Linq;
using Delimon.Win32.IO;

namespace AutoRenamer
{
    public class FileMover : IFileMover
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
                    CreateDir(folderPath);
                File.Move(from, to);
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e);
                return false;
            }
        }

        public static void CreateDir(string folderPath)
        {
            var parts = folderPath.Split('\\');
            for (int i = 1; i <= parts.Length; i++)
            {
                var subPath = String.Join("\\", parts.Take(i));
                if (!Directory.Exists(subPath))
                    Directory.CreateDirectory(subPath);
            }

        }
    }
}