using ConsoleAppBlobStorage.Infrastructure;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace ConsoleAppBlobStorage
{
    public static class BlobStorage
    {
        private const string CONTAINERPREFIX = "RandoPics";

        public static void CallBlobGettingStartedSamples()
        {
            BasicStorageBlockBlobOperationsAsync().Wait();
        }

        private static async Task BasicStorageBlockBlobOperationsAsync()
        {
            const string ImageToUpload = RenderedImage;

            Console.WriteLine( ImageToUpload);

            CloudStorageAccount storageAccount = StorageAccountSettings.CreateStorageAccountFromConnectionString();

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(CONTAINERPREFIX);
            try
            {
                // Retry policy - optional
                BlobRequestOptions optionsWithRetryPolicy = new BlobRequestOptions() { RetryPolicy = new Microsoft.Azure.Storage.RetryPolicies.LinearRetry(TimeSpan.FromSeconds(20), 4) };

                await container.CreateIfNotExistsAsync(optionsWithRetryPolicy, null);
            }
            catch (StorageException ex)
            {
                Console.WriteLine(ex.Message);
            }

            // await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            // Upload a BlockBlob(.png) to the newly created container
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(ImageToUpload);

            // Browser now knows it as an image.
            blockBlob.Properties.ContentType = "image/png";

            try
            {
                await blockBlob.UploadFromFileAsync(ImageToUpload);
            }
            catch (StorageException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
