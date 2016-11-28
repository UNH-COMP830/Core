using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameSlam.Infrastructure.Repositories;
using System.Configuration;         
using System.Text;
using GameSlam.Core.Models;

namespace GameSlam.Infrastructure.Tests
{
    [TestClass]
    public class BlobStorageRepositoryTest
    {

        public BlobStorageRepositoryTest()
        {     
            ConfigurationManager.AppSettings["StorageConnectionString"] = "DefaultEndpointsProtocol=https;AccountName=gameslamblob;AccountKey=mJM1+mBQQP/xdfmBN0rORGZnrVOw18uLZ/za88L55gTqoODyqQ3lzYL/RKY9MsB9vapxUYZcsYFk8y3R1Yn2DA==;";
        }


        /// <summary>
        /// print out files within a directory within Azure blog
        /// </summary>
        [TestMethod]
        public void GetAllBlogItemsTest()
        {    
            // store the default database values.
            BlobStorageRepository bs = new BlobStorageRepository();
            DownloadFileDetails fileDetails = bs.GetAllBlogItems("26780ba1-9c8f-442a-bb13-e0901b9c2a20");

            if (fileDetails == null)
                Assert.Fail("no File loaded");
        }

        [TestMethod]
        public void AddNewGameFilesTest()
        {
            // store the default database values.
            BlobStorageRepository bs = new BlobStorageRepository();

            UploadFileDetails toUpload = new UploadFileDetails();


            toUpload.Screenshots.Add(new UploadFileDetail() {
                FileData = Encoding.ASCII.GetBytes("Hello 1"),
                FileExtention  = ".txt"
            });

            toUpload.Screenshots.Add(new UploadFileDetail()
            {
                FileData = Encoding.ASCII.GetBytes("Hello 2"),
                FileExtention = ".html"
            });

            toUpload.Screenshots.Add(new UploadFileDetail()
            {
                FileData = Encoding.ASCII.GetBytes("Hello 3"),
                FileExtention = ".txt"
            });

            toUpload.WindowsDetails = new UploadFileDetail()
            {
                FileData = Encoding.ASCII.GetBytes("prog1"),
                FileExtention = ".txt"
            };

            String newGuid = bs.AddNewGameFiles(toUpload);

            if (newGuid == null)
                Assert.Fail("GUID should not be null");
            if (newGuid.Length == 0)
                Assert.Fail("GUID should not be empty");
            System.Diagnostics.Debug.WriteLine("Created new guid: " + newGuid);
        }

        /// <summary>
        /// Delete the Blog COntainer
        /// </summary>
        [TestMethod]
        public void DeleteEntireDirectoryTest()
        {
            // store the default database values.
            BlobStorageRepository bs = new BlobStorageRepository();
            bs.DeleteEntireDirectory("da1cd745-a8d9-43b0-bb28-0a421e6afbb4");
            bs.DeleteEntireDirectory("e276414c-924a-4529-a45c-88cfef89ab71");
        }
    }
}
