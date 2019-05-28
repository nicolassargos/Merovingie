using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC.Common.Descriptors;
using AoC.Common.Interfaces;
using Common.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AoC.DataLayer
{
    public class AzureGameFileManager : IGameFileManager
    {
        private readonly StorageCredentials credentials;
        private readonly CloudStorageAccount storageAccount;
        private readonly string containerName;
        private string storageConnectionString;

        public AzureGameFileManager(IConfiguration config)
        {
            credentials = new StorageCredentials(config["BlobStorage:Account"], config["BlobStorage:Key"]);
            storageConnectionString = config["BlobStorage:StorageConnectionString"];
            CloudStorageAccount.TryParse(storageConnectionString, out storageAccount);
            containerName = config["BlobStorage:ContainerName"];

            //DOC https://docs.microsoft.com/en-us/azure/storage/blobs/storage-upload-process-images?tabs=dotnet#configure-web-app-settings
            //StorageCredentials storageCredentials = new StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey);

            //// Create cloudstorage account by passing the storagecredentials
            //CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);
        }

        public void DeleteGame(string fileName)
        {
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get reference to the blob container by passing the name by reading the value from the configuration (appsettings.json)
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            try
            {
                if (container != null)
                {
                    container.GetBlockBlobReference(fileName)
                        .DeleteIfExists(); //TODO Async
                    //.DeleteIfExistsAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<GameDetailsDto> GetGameFiles()
        {
            var ReturnValue = new List<GameDetailsDto>();

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get reference to the blob container by passing the name by reading the value from the configuration (appsettings.json)
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            // List the blobs in the container.
            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var results = container.ListBlobsSegmented(null, blobContinuationToken);//TODO Assync
                //var results = await container.ListBlobsSegmentedAsync(null, blobContinuationToken);
                // Get the value of the continuation token returned by the listing call.
                blobContinuationToken = results.ContinuationToken;
                foreach (IListBlobItem item in results.Results)
                {
                    Console.WriteLine(item.Uri);
                    ReturnValue.Add(new GameDetailsDto { Name = item.Uri.Segments.Last(), Path = item.Uri.ToString(), CreationDate = DateTime.Now });  //TODO Right values in the righ place
                }
            } while (blobContinuationToken != null); // Loop while the continuation token is not null.

            return ReturnValue;

        }

        public IGameDescriptor ReadGame(string fileName)
        {
            Stream ReturnValue = new MemoryStream();

            if (fileName.IsGameFileNameValidWithThrow())
            {
                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Get reference to the blob container by passing the name by reading the value from the configuration (appsettings.json)
                CloudBlobContainer container = blobClient.GetContainerReference(containerName);

                try
                {
                    if (container != null)
                    {

                        container.GetBlockBlobReference(fileName)
                            .DownloadToStream(ReturnValue);  //TODO Async
                                                             //.DownloadToStreamAsync();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return ReturnValue.GetGameDescriptor();
        }

        public string SaveGame(IGameDescriptor game, string fileName)
        {
            string ReturnValue = string.Empty;

            if (game.IsGameDescriptorValidWithThrow() && fileName.IsGameFileNameValidWithThrow())
            {
                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Get reference to the blob container by passing the name by reading the value from the configuration (appsettings.json)
                CloudBlobContainer container = blobClient.GetContainerReference(containerName);

                // Get the reference to the block blob from the container
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

                ReturnValue = blockBlob.Uri.ToString();

                // Upload the file
                blockBlob.UploadFromStream(game.GetGameDescriptorStream());
                //Task.Run( async () => await blockBlob.UploadFromStreamAsync(game.GetGameDescriptorStream()));  //TODO Assync
            }
            return ReturnValue;
        }
    }
}
