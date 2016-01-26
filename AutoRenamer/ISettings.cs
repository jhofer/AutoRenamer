using System;
using System.Collections.Generic;


namespace AutoRenamer
{
    public interface ISettings
    {



        List<String> Extensions { get; }
   
        String SourcePath { get; }

        String MoviePath { get; }
        String SeriesPath { get; }
        String FileBotInstallDir { get; }
        String ArchivePath { get; }
        String SeriesFormat { get; }
        String MoviesFormat { get; }
        String MoviesDB { get; }
        String SeriesDB { get; }
        String MovieLang { get; }
        String SeriesLang { get; }

        int Sleep { get; }

        string SeriesIdentifierRegex { get; }

     
    }
}