using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileUtilities
{
    public class FileHandlerTDD
    {
        public ILog Logger;

        public FileHandlerTDD(ILog logger)
        {
            Logger = logger;
        }

        public void DeleteFilesNotInUse(string fileDirectory)
        {
            var directoryInfo = new DirectoryInfo(fileDirectory);

            foreach (var file in directoryInfo.GetFiles())
            {
                if (FileIsInUse(file))
                {
                    Logger.Log(file.Name + " appears to be in use.");
                }
                else
                {
                    file.Delete();
                }
            }
        }

        private bool FileIsInUse(FileInfo file)
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
