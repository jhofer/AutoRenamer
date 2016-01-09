using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AutoRenamer
{
    public class FileBot : IFileBot
    {
        private readonly bool isMovie;
        private readonly ILogger logger;
        private readonly ISettings settings;
        private readonly Regex rgx;

        public FileBot(ISettings settings, ILogger logger, bool ismovie)
        {
            this.settings = settings;
            this.logger = logger;
            this.isMovie = ismovie;
            this.rgx = new Regex(settings.SeriesIdentifierRegex);
        }

        public bool Rename(string filepath, out string newFileName)
        {
            newFileName = "";
            if (DoRename(filepath))
            {
                try
                {
                    var proc = CreateProcess(filepath);

                    proc.Start();
                    proc.WaitForExit();
                    StringBuilder builder = new StringBuilder();

                    var renamed = false;
                    while (!proc.StandardOutput.EndOfStream)
                    {
                        string line = proc.StandardOutput.ReadLine();

                        builder.Append(line);
                        if (line.Contains("[MOVE] Rename"))
                        {
                            var parts = line.Split('[');
                            var lastPart = parts[parts.Length - 1];
                            newFileName = lastPart.Substring(0, lastPart.Length - 1);
                            renamed = true;
                        }
                    }

                    var str = builder.ToString();

                    if (str.Contains("Failure"))
                    {
                        logger.Error(str);
                        return false;
                    }
                    else
                    {
                        logger.Info(str);
                        return renamed;
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private string CreateParameters(string filepath)
        {
            var lang = "";
            var db = "";
            var format = "";
            if (isMovie)
            {
                lang = settings.MovieLang;
                db = settings.MoviesDB;
                format = settings.MoviesFormat;
            }
            else
            {
                lang = settings.SeriesLang;
                db = settings.SeriesDB;
                format = settings.SeriesFormat;
            }
            string parameters = $"-rename \"{filepath}\" --lang {lang} --db {db} --format \"{format}\"";
            if (!isMovie)
            {
                parameters = parameters + " -non-strict";
            }
            return parameters;
        }

        private Process CreateProcess(string filepath)
        {
            string fileBotPath = settings.FileBotInstallDir + "\\filebot.exe";
            if (!File.Exists(fileBotPath))
            {
                throw new Exception($"Filebot installation not found at {fileBotPath}. Please check 'fileBotInstallDir' in the appsettings.");
            }
            var procStIfo = new ProcessStartInfo(settings.FileBotInstallDir + "\\filebot.exe", CreateParameters(filepath));
            procStIfo.RedirectStandardOutput = true;
            procStIfo.RedirectStandardError = true;
            procStIfo.UseShellExecute = false;
            procStIfo.CreateNoWindow = false;
            procStIfo.StandardOutputEncoding = System.Text.Encoding.Default;

            var proc = new Process();
            proc.StartInfo = procStIfo;
            return proc;
        }

        private bool DoRename(string filepath)
        {
            return (isMovie && !rgx.IsMatch(filepath)) || (!isMovie && rgx.IsMatch(filepath));
        }
    }
}