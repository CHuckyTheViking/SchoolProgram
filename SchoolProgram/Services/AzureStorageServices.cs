using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace SchoolProgram.Services
{
    public class AzureStorageServices
    {

        private static readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=jm23sa;AccountKey=TCeFeu3/n8QMNkEhiSWtm1Pj7fvWZQ3FEPHCcYKGe5tvLBNiC8wEP4VlfzMZr8sy2MIMknmFKTs8DTUTpqbJPQ==;EndpointSuffix=core.windows.net";

        public static async Task<string> UploadPictureAsync(IFormFile file)
        {
            string url = "";

            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("schoolprogrampictures");

                await container.CreateIfNotExistsAsync();

                await container.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

                CloudBlockBlob blob = container.GetBlockBlobReference(file.FileName);

                await blob.UploadFromStreamAsync(file.OpenReadStream());

                var uri = container.GetBlockBlobReference(file.FileName).SnapshotQualifiedUri;

                url = uri.ToString();

            }
            catch { }

            return url;
        }
    }
}
