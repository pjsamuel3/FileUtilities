using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileUtilities;
using System.IO;

namespace FileHandlerTests
{
    [TestClass]
    public class FileHanderTests// : Extensions<FileHanderTests>
    {
        [TestInitialize]
        public void Setup()
        {
            logger = new LoggerMock();
        }

        [TestMethod]
        public void Can_Create_Instance_Of_File_Handler()
        {
            fileHandler = new FileHandlerTDD(logger);

            Assert.IsNotNull(fileHandler);
        }

        [TestMethod]
        public void Can_Delete_File()
        {
            //Given
            We_Have_A_File_Handler();
            We_Have_A_File();

            The_File_Exists();

            //When
            The_File_Is_Deleted();

            //Then
            The_File_Should_Not_Exist();

        }

        [TestMethod]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void Handles_Error_If_Folder_Does_Not_Exist()
        {
            //Given
            We_Have_A_File_Handler();
            We_Have_A_Non_Existant_File();

            //When
            The_File_Is_Deleted();

            //Then
            //DirectoryNotFoundException Thrown
        }

        [TestMethod]
        public void Does_Not_Delete_Files_In_Use()
        {
            //Given
            We_Have_A_File_Handler();
            We_Have_A_File_In_Use();

            //When
            The_File_Is_Deleted();

            //Then
            Assert.AreEqual("File3.txt appears to be in use.", logger.Message);
        }

        [TestCleanup]
        public void Teardown()
        {
            file.Close();
        }

        private void We_Have_A_File_In_Use()
        {
            fileName = "File3.txt";
            fileDirectory = @"C:\temp\unittests\";
            file = File.Open(filePath, FileMode.OpenOrCreate);
            file.Lock(0, 0);
        }

        private void We_Have_A_Non_Existant_File()
        {
            fileName = @"test2.txt";
            fileDirectory = @"C:\temp\doesnotexist\";
        }

        private void The_File_Exists()
        {
            Assert.IsTrue(file.CanRead);
            file.Close();
        }

        private void The_File_Should_Not_Exist()
        {
            Assert.IsFalse(file.CanRead);
            //file.Should_Be_Null();
        }

        private void The_File_Is_Deleted()
        {
            fileHandler.DeleteFilesNotInUse(fileDirectory);
        }

        private void We_Have_A_File()
        {
            fileName = @"test1.txt";
            fileDirectory = @"C:\temp\unittests\";
            file = File.Open(filePath, FileMode.OpenOrCreate);
        }

        private void We_Have_A_File_Handler()
        {
            fileHandler = new FileHandlerTDD(logger);
        }



        FileHandlerTDD fileHandler;

        FileStream file;

        string fileName;
        string fileDirectory;

        private string filePath
        {
            get
            {
                return fileDirectory + fileName;
            }
        }

        string message;

        LoggerMock logger;

    }

    public static class Extensions
    {
        //public static void Should_Be_Null(this object obj)
        //{
        //    Assert.IsNull(obj);
        //}

        //public static void Be_like(this object obj, object value)
        //{
        //    Assert.AreEqual(value, obj);
        //}
    }
}
