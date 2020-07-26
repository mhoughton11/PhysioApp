using DIYHIIT.Models.Exercise;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using static DIYHIIT.Models.Constants;

namespace DIYHIIT.Services
{
    class BlobStorageService
    {
        readonly static CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(STORAGE_ACCOUNT_KEY1);
        readonly static CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

        static public async Task<byte[]> GetImageBytes(string exerciseName)
        {
            // Get blobs from image container
            var blobs = await GetBlobs<CloudBlockBlob>("images");

            var imageName = exerciseName + ".jpg";

            // Cycle through blobs, if image name equals blob name, download and return.
            foreach (var blob in blobs)
            {
                if (blob.Name == imageName)
                    return await DownloadBlobData(blob);
            }
            return null;
        }

        static public async Task<List<Exercise>> GetExerciseData(List<CloudBlockBlob> blobs)
        {
            List<Exercise> items = new List<Exercise>();

            foreach (var blob in blobs)
            {
                // Download blob data
                var bytes = await DownloadBlobData(blob);

                // Write to local JSON file
                var filename = Path.Combine(App.FolderPath, blob.Name);
                File.WriteAllBytes(filename, bytes);

                // Extract deserialized objects from local file.
                //items.AddRange(GetJsonData(blob.Name));
            }

            return items;
        }

        static public async Task<List<T>> GetBlobs<T>(string containerName, string prefix = "", int? maxresultsPerQuery = null, BlobListingDetails blobListingDetails = BlobListingDetails.None) where T : ICloudBlob
        {
            // Retrieve reference to container
            var blobContainer = cloudBlobClient.GetContainerReference(containerName);

            var blobList = new List<T>();

            BlobContinuationToken continuationToken = null;

            try
            {
                do
                {
                    var response = await blobContainer.ListBlobsSegmentedAsync(prefix, true, blobListingDetails, maxresultsPerQuery, continuationToken, null, null);

                    continuationToken = response?.ContinuationToken;

                    foreach (var blob in response?.Results?.OfType<T>())
                    {
                        blobList.Add(blob);
                    }

                } while (continuationToken != null);
            }
            catch (Exception ex)
            {
                // Handle exception
                Debug.WriteLine("Blob download failed with exception: " + ex.Message);
            }

            return blobList;
        }

        public static async Task<byte[]> SaveBlockBlob(string containerName, byte[] blob, string blobTitle)
        {
            try
            {
                var blobContainer = cloudBlobClient.GetContainerReference(containerName);
                var blockBlob = blobContainer.GetBlockBlobReference(blobTitle);

                await blockBlob.UploadFromByteArrayAsync(blob, 0, blob.Length);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to save {blobTitle} to cloud storage: {ex.Message}");
            }

            return blob;
        }

        public static async Task<bool> DeleteBlob(string blobName, string containerName)
        {
            bool result;

            try
            {
                var blobContainer = cloudBlobClient.GetContainerReference(containerName);

                var blob = blobContainer.GetBlockBlobReference(blobName);
                result = await blob.DeleteIfExistsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Couldn't delete {blobName}: {ex.Message}");
                result = false;
            }

            return result;
        }

        public static async Task<byte[]> DownloadBlobData(CloudBlockBlob blob)
        {
            var info = blob.Properties;
            var buffer = new byte[info.Length];

            await blob.DownloadToByteArrayAsync(buffer, 0);

            return buffer;
        }
    }
}