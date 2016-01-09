using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace AutoRenamer
{
    class Settings : ISettings
    {
        private readonly string archivePath;
        private readonly List<string> extensions;
        private readonly string filebotInstallDir;
        private readonly string moviesDB;
        private readonly string moviesFormat;
        private readonly string moviesLang;
        private readonly string moviesPath;
        private readonly string seriesDB;
        private readonly string seriesFormat;
        private readonly string seriesLang;
        private readonly string seriesPath;
        private readonly int sleep;
        private readonly string sourcePath;
        private readonly string seriesIdentifierRegex;

        public Settings(ILogger logger)
        {
            this.extensions = ConfigurationSettings.AppSettings["extensions"].Split(',').ToList();
            this.sourcePath = ConfigurationSettings.AppSettings["sourcePath"];
            this.moviesPath = ConfigurationSettings.AppSettings["moviesPath"];
            this.seriesPath = ConfigurationSettings.AppSettings["seriesPath"];
            this.filebotInstallDir = ConfigurationSettings.AppSettings["filebotInstallDir"];
            this.archivePath = ConfigurationSettings.AppSettings["archivePath"];
            this.seriesFormat = ConfigurationSettings.AppSettings["seriesFormat"];
            this.moviesFormat = ConfigurationSettings.AppSettings["moviesFormat"];
            this.moviesDB = ConfigurationSettings.AppSettings["moviesDB"];
            this.seriesDB = ConfigurationSettings.AppSettings["seriesDB"];
            this.moviesLang = ConfigurationSettings.AppSettings["moviesLang"];
            this.seriesLang = ConfigurationSettings.AppSettings["seriesLang"];
            this.sleep = Int32.Parse(ConfigurationSettings.AppSettings["sleep"]);
            this.seriesIdentifierRegex = (ConfigurationSettings.AppSettings["seriesIdentifierRegex"]);
       

            logger.Info($"Settings Loaded: \n "
                        + $"extensions: {String.Join(" ",this.Extensions)} \n"
                        + $"sourcePath: {this.sourcePath} \n"
                        + $"moviesPath: {this.moviesPath} \n"
                        + $"seriesPath: {this.seriesPath} \n"
                        + $"filebotInstallDir: {this.filebotInstallDir} \n"
                        + $"archivePath: {this.archivePath} \n"
                        + $"seriesFormat: {this.seriesFormat} \n"
                        + $"moviesFormat: {this.moviesFormat} \n"
                        + $"moviesDB: {this.moviesDB} \n"
                        + $"seriesDB: {this.seriesDB} \n"
                        + $"moviesLang: {this.moviesLang} \n"
                        + $"seriesLang: {this.seriesLang} \n"
                        + $"seriesIdentifierRegex: {this.seriesIdentifierRegex} \n"
                        + $"sleep: {this.sleep} \n"
                );
        }

        public List<string> Extensions
        {
            get { return this.extensions; }
        }

        public string SourcePath
        {
            get { return this.sourcePath; }
        }

        public string MoviePath
        {
            get { return this.moviesPath; }
        }

        public string SeriesPath
        {
            get { return this.seriesPath; }
        }

        public string FileBotInstallDir
        {
            get { return this.filebotInstallDir; }
        }

        public string ArchivePath
        {
            get { return this.archivePath; }
        }

        public string SeriesFormat
        {
            get { return this.seriesFormat; }
        }

        public string MoviesFormat
        {
            get { return this.moviesFormat; }
        }

        public string MoviesDB
        {
            get { return this.moviesDB; }
        }

        public string SeriesDB
        {
            get { return this.seriesDB; }
        }

        public string MovieLang
        {
            get { return this.moviesLang; }
        }

        public string SeriesLang
        {
            get { return this.seriesLang; }
        }

        public int Sleep
        {
            get { return this.sleep; }
        }

        public string SeriesIdentifierRegex {
            get { return this.seriesIdentifierRegex; }
        }
    }
}