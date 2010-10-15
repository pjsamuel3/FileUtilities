using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileUtilities
{
    public class Logger : ILog
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
