using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;

namespace FileUtilities
{
    public class SimpleFileHelper
    {
        public static void Main(string[] args)
        {
            DateTime startTime = DateTime.Now;

            if (FileExists(args))
            {
                foreach (string argument in args)
                {
                    Console.WriteLine(argument);
                    ReplaceInFile(argument, ",", ";");
                }
            }

            DisplayElapsedTime(startTime);

            Console.ReadLine();
        }

        private static bool FileExists(string[] arguments)
        {
            bool canContinue = true;

            if (arguments.Count<String>() < 1)
            {
                Console.WriteLine("Please specify at least one file path");
                canContinue = false;
            }

            foreach (var path in arguments)
            {
                FileInfo fi = new FileInfo(path);
                if (!fi.Exists)
                {
                    Console.WriteLine("{0} does not exist!", path);
                    canContinue = false;
                }
            }

            return canContinue;
        }

        public static void ReplaceInFile(string filePath, string searchText, string replaceText)
        {
            string content = GetFileContents(filePath);

            CountReplacements(searchText, content);

            content = Regex.Replace(content, searchText, replaceText);

            WriteToFile(filePath, content);

        }
        
        private static string GetFileContents(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            string content = reader.ReadToEnd();
            reader.Close();
            return content;
        }

        private static void WriteToFile(string filePath, string content)
        {
            StreamWriter writer = new StreamWriter(filePath);
            writer.Write(content);
            writer.Close();
        }

        private static void CountReplacements(string searchText, string content)
        {
            int counter = 0;

            foreach (char found in content)
            {
                var charArr = searchText.ToCharArray();
                int i = charArr.Count();

                if (i == 1)
                {
                    if (found == charArr[0])
                    {
                        counter++;
                    }
                }

            }

            Console.WriteLine("Replacing {0} instances of {1}", counter, searchText);
        }

        private static void DisplayElapsedTime(DateTime startTime)
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            Console.WriteLine("Operation took: {0} seconds", elapsed.TotalSeconds);
        }
    }
}
