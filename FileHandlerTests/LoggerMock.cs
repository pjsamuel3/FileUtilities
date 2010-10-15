using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using FileUtilities;

namespace FileHandlerTests
{
    public class LoggerMock : ILog
    {
        public string Message { get; set; }
        public bool IsMessageSet { get; set; }

        public void Log(string message)
        {
            Message = message;
            IsMessageSet = true;
        }
    }
}
