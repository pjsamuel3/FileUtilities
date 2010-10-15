using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileUtilities
{
    public class FileHandler
    {
        public void DeleteFilesNotInUse(string path)
        {
            var files = GetAllFilesFromPath(path);

            foreach (var fileInfo in files)
            {
                if (IsFileLocked(fileInfo))
                {
                    Console.WriteLine("File " + fileInfo.Name + " appears to be in use.");
                }
                else
                {
                    Console.WriteLine("Deleting: " + fileInfo.Name);
                    fileInfo.Delete();
                }
            }
        }

        public void MoveFiles(string sourceFolderPath, string destinationFolderPath)
        {
            CheckDirectoryExists(sourceFolderPath);
            CheckDirectoryExists(destinationFolderPath);

            var sourceFiles = GetAllFilesFromPath(sourceFolderPath);
            var fullArchivePath = sourceFolderPath + @"\Arkiv\";
            
            foreach (var file in sourceFiles)
            {
                if (FileExistsAtDestination(file, destinationFolderPath))
                {
                    FileExistsAtDestinationWarning(file.Name);
                    MoveToArchive(file, fullArchivePath);
                }
                else
                {
                    CopyToNewLocation(file, destinationFolderPath);
                    MoveToArchive(file, fullArchivePath);
                }
            }
        }
        
        private FileInfo[] GetAllFilesFromPath(string sourceFolder)
        {
            var directoryInfo = new DirectoryInfo(sourceFolder);

            return directoryInfo.GetFiles();
        }

        private void FileExistsAtDestinationWarning(string filename)
        {
            Console.WriteLine(String.Format("The filename {0} already exists in the destination folder.",filename));

            //string nl = "\n";

            //string feilmelding =
            //        "LoadId: " + Dts.Variables["User::LoadId"].Value.ToString() + nl +
            //        Dts.Variables["System::ErrorDescription"].Value.ToString() + nl +
            //        "FileName: " + Dts.Variables["User::Filnavn"].Value.ToString() + nl +
            //        "PackageName: " + Dts.Variables["System::PackageName"].Value.ToString() + nl +
            //        "SourceName: " + Dts.Variables["System::SourceName"].Value.ToString();

            //ModulisException e = new ModulisException(feilmelding);

            ////Her legges inn gitt tallkode fra Feilmeldingsdatabasen
            //Logging.Skriv(10720, e);
        }

        private void CopyToNewLocation(FileInfo fileToCopy, string destinationPath)
        {
            string oldPath = fileToCopy.FullName;
            var newFullPath = oldPath.Replace(fileToCopy.DirectoryName.ToString(), destinationPath);
            fileToCopy.CopyTo(newFullPath, false);
        }

        private void MoveToArchive(FileInfo fileToArchive, string fullArchivePath)
        {
            EnsureDirectoryExists(fullArchivePath);
            fileToArchive.MoveTo(fullArchivePath + fileToArchive.Name);
        }

        private void EnsureDirectoryExists(string fullArchivePath)
        {
            DirectoryInfo dir = new DirectoryInfo(fullArchivePath);

            if (!dir.Exists)
            {
                dir.Create();
            }
        }

        private void CheckDirectoryExists(string directory)
        {
            DirectoryInfo dir = new DirectoryInfo(directory);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException();
            }
        }

        private bool FileExistsAtDestination(FileInfo file, string locationPath)
        {
            var locationFiles = GetAllFilesFromPath(locationPath);

            foreach (var destFile in locationFiles)
            {
                if (destFile.Name == file.Name)
                {
                    return true;
                }
            }

            return false;
        }

        protected bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }


    }
}
