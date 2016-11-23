using GameSlam.Core.Models;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using System;
using System.Collections.Generic;
using System.Configuration;

namespace GameSlam.Infrastructure.Repositories
{
    public class BlobStorageRepository
    {
        private readonly CloudStorageAccount storageAccount;
        private readonly Object myLock = new Object();
        private const String snapshotPrefix = "photo_";
        private const String programPrefix = "app";

        public BlobStorageRepository()
        {
            // Parse the connection string and return a reference to the storage account.
            storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //string value = ConfigurationManager.AppSettings["BlogStorageEndpoint"];
        }

        public CloudBlobContainer GetContainer(string guid)
        {
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference(guid);
                                                                     
            if (!container.Exists())
                return null;
            return container; 
        }

        /// <summary>
        /// Creates a new Blog storage container and places the files inside of them.
        /// </summary>
        /// <param name="files"></param>
        /// <param name="program"></param>
        /// <returns>new Guid</returns>
        public string AddNewGameFiles(List<UploadFileDetails> fileData, UploadFileDetails program)
        {
            String newGuid = string.Empty;    
                       
            lock (myLock)
            {
                // Retrieve a reference to a container.
                CloudBlobContainer container = getNewContainer();
                newGuid = container.Name;

                UploadBlobToContainer(container, fileData, program);
            }
            return newGuid;        
        }

        private CloudBlobContainer getNewContainer()
        {
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            while (true)
            {
                String guid = GenerateGuid();
                // Retrieve a reference to a container.
                CloudBlobContainer container = blobClient.GetContainerReference(guid);
                if(container.CreateIfNotExists())
                {
                    container.SetPermissions(
                    new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                    return container;
                }
            }
        }

        /// <summary>
        /// Generates a new Guid string
        /// </summary>
        /// <returns></returns>
        private String GenerateGuid()
        {
            Guid g;
            // Create and display the value of two GUIDs.
            g = Guid.NewGuid();
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Upload the files to the container.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="imageFile"></param>
        /// <param name="program"></param>
        private void UploadBlobToContainer(CloudBlobContainer container, List<UploadFileDetails> imageFile, UploadFileDetails program)
        {  
            for (int i = 0; i < imageFile.Count; i++)
            {
                UploadFileDetails singleFile = imageFile[i];
                if (singleFile == null)
                    continue;
                AddFileHelper(container, singleFile, snapshotPrefix + i + singleFile.FileExtention);
            }

            if (program != null)
                AddFileHelper(container, program, programPrefix + program.FileExtention);
        }

        /// <summary>
        /// Stores the file
        /// </summary>
        /// <param name="container"></param>
        /// <param name="singleFile"></param>
        /// <param name="fileName"></param>
        private void AddFileHelper(CloudBlobContainer container, UploadFileDetails singleFile, String fileName)
        {
            if (singleFile == null)
                return;
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            blockBlob.UploadFromByteArray(singleFile.FileData, 0, singleFile.FileData.Length);
        }

        /// <summary>
        /// Returns all the file links stored within this container
        /// </summary>
        /// <param name="guid"></param>
        public DownloadFileDetails GetAllBlogItems(string guid)
        {
            return GetAllBlogItems(GetContainer(guid));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        private DownloadFileDetails GetAllBlogItems(CloudBlobContainer container)
        {   
            if (container == null)
                return new DownloadFileDetails();

            DownloadFileDetails downloadDetails = new DownloadFileDetails();

            // Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                FileStorageDetail storageDetail = null;

                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    storageDetail = new FileStorageDetail()
                    {
                        FileSize = blob.Properties.Length,
                        FileUrl = blob.Uri.ToString()
                    };                                                                                                     

                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;

                    storageDetail = new FileStorageDetail()
                    {
                        FileSize = pageBlob.Properties.Length,
                        FileUrl = pageBlob.Uri.ToString()
                    };

                }
                //else if (item.GetType() == typeof(CloudBlobDirectory))
                //{
                //    CloudBlobDirectory directory = (CloudBlobDirectory)item;

                //    storageDetail = new FileStorageDetail()
                //    {   
                //        FileUrl = directory.Uri.ToString()
                //    };
                //}

                if(storageDetail != null)
                {
                    Uri uri = new Uri(storageDetail.FileUrl);
                    string filename = System.IO.Path.GetFileName(uri.LocalPath);
                    if (filename.StartsWith(programPrefix))
                    {
                        downloadDetails.ProgramFile = storageDetail;
                    }
                    else if (filename.StartsWith(snapshotPrefix))
                    {
                        downloadDetails.Screenshots.Add(storageDetail);
                    }
                }
            }
            return downloadDetails;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="names"></param>
        public void DeleteBlobFiles(String guid, List<string> names)
        {   
            DeleteBlobFiles(GetContainer(guid), names);
        }

        /// <summary>
        ///  Deleted the file names if it exist in the Blob
        /// </summary>
        /// <param name="container"></param>
        /// <param name="names"></param>
        private void DeleteBlobFiles(CloudBlobContainer container, List<string> names)
        {         
            if (container == null)
                return;

            if (names == null || names.Count == 0)
                return;
            
            foreach(string name in names)
            {
                try
                {
                    // Retrieve reference to a blob named "myblob.txt".
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(name);

                    if(blockBlob != null)
                    {
                        // Delete the blob.
                        blockBlob.Delete();
                        System.Diagnostics.Debug.WriteLine("deleted Blog: ", name);
                    }
                    
                }
                catch  (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception occured:" + ex.Message);
                }                
            }             
        }

        /// <summary>
        /// Deletes the container with the specified name
        /// </summary>
        /// <param name="guid"></param>
        public void DeleteEntireDirectory(string guid)
        {
            DeleteEntireDirectory(GetContainer(guid));
        }
        /// <summary>
        /// Delete the Blog container
        /// </summary>
        /// <param name="container"></param>
        private void DeleteEntireDirectory(CloudBlobContainer container)
        { 
            if (container == null)
                return;
            container.Delete();
        }
    }
}    
