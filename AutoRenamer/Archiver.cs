using Delimon.Win32.IO;

namespace AutoRenamer
{
    class Archiver : IArchiver
    {
        private readonly IFileMover fileMover;
        private readonly ISettings settings;

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
                if (!Directory.Exists(settings.ArchivePath))
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