using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRenamer
{
 
    class Archiver : IArchiver
    {
        private readonly ISettings settings;
        private readonly IFileMover fileMover;

        public Archiver(ISettings settings, IFileMover fileMover)
        {
            this.settings = settings;
            this.fileMover = fileMover;
        }

        public void ArchiveExisting(string filePath)
        {
            if (File.Exists(filePath))
            {
                
                var fileName = Path.GetFileName(filePath);
                var archiveFilePath = settings.ArchivePath + "\\" + fileName;
                Directory.CreateDirectory(settings.ArchivePath);
                if (File.Exists(archiveFilePath))
                {
                    File.Delete(archiveFilePath);
                }
                fileMover.Move(filePath, archiveFilePath);
            }
        }

    }
}
