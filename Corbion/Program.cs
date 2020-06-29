using System;
using log4net;  // see: https://logging.apache.org/log4net/release/config-examples.html for log4net info
using log4net.Config;  // see: https://stackify.com/log4net-guide-dotnet-logging/ for tutorial
using System.IO;

// to be able to read from app.config these are necessary
using System.Configuration;
using System.Collections.Specialized;



namespace Corbion
{
    class Program
    {
        private static readonly log4net.ILog log = 
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {

            log.Info("Corbion automatic scanfile upload utility");
            // Get directory 
            string sourceFolder = ConfigurationManager.AppSettings.Get("ScanfilesFolder");
            string processedFolder = ConfigurationManager.AppSettings.Get("ProcessedFolder");

            //start filewatch
            FileSystemWatcher watcher = new FileSystemWatcher(sourceFolder);
            watcher.IncludeSubdirectories = true;
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;

            //watcher.Changed += new FileSystemEventHandler(watcher_changed);
            watcher.Created += new FileSystemEventHandler(watcher_changed);
            watcher.EnableRaisingEvents = true;
            bool foundFile = false;

            //log the directory's
            //log.Info("Scanfile sourcefolder (read from app.config): " + sourceFolder);
            //log.Info("Scanfile processedFolder (read from app.config): " + processedFolder);
            //log.Info(watcher.Path);

            Console.WriteLine("Watchting for files.... press <x> to exit.");

            while (!foundFile)
            {
                // Start a console read operation. Do not display the input.
                ConsoleKeyInfo cki = Console.ReadKey(true);

                // Announce the name of the key that was pressed .
                log.Info(String.Format("  Key pressed: {0}\n", cki.Key.ToString()));

                // Exit if the user pressed the 'X' key.
                if (cki.Key == ConsoleKey.X) break;
            }

            

            //check the directory for file's
          

            // stuur bestand op naar ingestelde url

            // geef aan of het bestand goed is ontvangen door de server

            // zo ja, verplaats het verstuurde bestand naar een ingestelde plek waar verwerkte bestnaden horen ('verwerkt')
        }

        static void watcher_changed(Object sender, FileSystemEventArgs e)
        {
            string[] readText = File.ReadAllLines(e.FullPath);
            foreach (string s in readText)
            {
                log.Info(s);
            }
            //log.Info(String.Format("New file found: {0}", e.FullPath));
            // string destinationFile = ConfigurationManager.AppSettings.Get("ProcessedFolder") + @"\" + e.Name;
            // To move a file or folder to a new location:
            // File.Move(e.FullPath, destinationFile);
        }

        // Process all files in the directory passed in, recurse on any directories 
        // that are found, and process the files they contain.
        public static void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        // Insert logic for processing found files here.
        public static void ProcessFile(string path)
        {
            log.Info(String.Format("Processed file '{0}'.", path));
        }
    }
}
