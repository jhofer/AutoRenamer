# AutoRenamer
## What it does
Windowsservice which controlls the FileBot-commandline interfaces
It scans you folder for new movies/series. It renames the files and moves them to your library

##How to use
1. Download and install filebot on your system http://www.filebot.net/
2. Open the sln file with visual studio 2015+
3. Build the solution and start the Installer or directly download the setup in the "Installer" dir
4. Follow the wizard and install the windows service
5. Open the config file under: "C:\Program Files (x86)\Slop Software\AutoRenamer\AutoRenamer.exe.config"
6. Set the following value for your needs:

    `<add key="sourcePath" value="E:\Downloads"/>` location of the files to rename
    
    `<add key="moviesPath" value="Z:\Movies"/>` root destination for movies
    
    `<add key="seriesPath" value="Z:\Serien"/>` root destination for series
    
    `<add key="archivePath" value="E:\archive"/>` folder for the archive, if the file already exists the previews file is moved to the archive
    
    `<add key="filebotInstallDir" value="C:\Program Files\FileBot"/>` the installation dir of your filebot installation
    
    `<add key="extensions" value="*.mkv,*.avi,*.mp4"/>` filetypes to bother about
    
    `<add key="sleep" value="60000"/>` sleep time between scans (miliseconds)
    
    `<add key="seriesFormat" value="{n}/Season {s}/{n} - {s00e00} - {t}"/>` naming pattern for series  [filebot cli](http://www.filebot.net/cli.html)
    
    `<add key="moviesFormat" value="{n} ({y})/{n} ({y})"/>` naming pattern for movies [filebot cli](http://www.filebot.net/cli.html)
    
    `<add key="moviesDB" value="TheMovieDB"/>` the database for renaming movies [filebot cli](http://www.filebot.net/cli.html)
    
    `<add key="seriesDB" value="thetvdb"/>` the database for renaming series [filebot cli](http://www.filebot.net/cli.html)
    
    `<add key="moviesLang" value="de"/>` prefered language for movies
    
    `<add key="seriesLang" value="en"/>` prefered language for series 
    
    `<add key="seriesIdentifierRegex" value=".*[Ss][0-9][0-9][Ee][0-9][0-9].*"/>` regex to determ a serie file or not
    
