using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
