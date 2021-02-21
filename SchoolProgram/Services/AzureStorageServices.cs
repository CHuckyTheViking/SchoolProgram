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

        public static async Task UploadBackgroundAsync()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("schoolprogrampictures");

            await container.CreateIfNotExistsAsync();

            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            var exists = await container.GetBlockBlobReference("BackgroundBlurred.jpg").ExistsAsync();
            
            if (exists == false)
            {
                CloudBlockBlob blob = container.GetBlockBlobReference("BackgroundBlurred.jpg");
                await blob.UploadFromFileAsync("Images/Background/BackgroundBlurred.jpg");
            }

            var logoExists = await container.GetBlockBlobReference("SchoolLogo.png").ExistsAsync();
            if (logoExists == false)
            {
                CloudBlockBlob blob = container.GetBlockBlobReference("SchoolLogo.png");
                await blob.UploadFromFileAsync("Images/Logo/SchoolLogo.png");
            }

        }

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

                if (file != null)
                {
                    

                    CloudBlockBlob blob = container.GetBlockBlobReference(file.FileName);

                    await blob.UploadFromStreamAsync(file.OpenReadStream());
                    
                    var uri = container.GetBlockBlobReference(file.FileName).SnapshotQualifiedUri;

                    url = uri.ToString();
                }
                else
                {
                    
                    var exists = container.GetBlockBlobReference("defaultContact.png").ExistsAsync();

                    if (exists.Result == false)
                    {
                        CloudBlockBlob blob = container.GetBlockBlobReference("defaultContact.png");

                        await blob.UploadFromFileAsync("Images/Profile/defaultContact.png");
                        var uri = container.GetBlockBlobReference("defaultContact.png").SnapshotQualifiedUri;

                        url = uri.ToString();

                    }
                    else
                    {
                        var uri = container.GetBlockBlobReference("defaultContact.png").SnapshotQualifiedUri;
                        url = uri.ToString();
                    }
                }

            }
            catch { }

            return url;
        }
    }
}
