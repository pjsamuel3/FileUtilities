using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileUtilities
{
    class Program
    {
        static void Main(string[] args)
        {
            //for (int i = 0; i < args.Length; i++)
            //{
            //    Console.WriteLine(string.Format("Deleting files not is use from path: {0}", args[i]));
                
            //    try
            //    {
            //        var fileHandler = new FileHandler();
            //        //fileHandler.DeleteFilesNotInUse(args[i]);
            //        fileHandler.MoveFilesInFolder("", "");
            //    }
            //    catch (Exception exception)
            //    {
            //        Console.WriteLine("Error: " + exception.Message);
            //    }
                
            //}

            var fileHandler = new FileHandler();
            fileHandler.MoveFiles(@"C:\Projects\Torgeir\SR_1004_LikningsdataBestilling", @"C:\Projects\Torgeir\SR_1004");
            Console.ReadLine();
        }
    }
}
