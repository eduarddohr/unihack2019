using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ResoApi
{
    public class AzureHelper
    {
        public static void Upload()
        {
            CloudStorageAccount AzureStorage = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=resoapi;AccountKey=K180XLzVYBZnTtD72f6TzqPNGOb9RMa29O7fv5GZYkQ8MQRVH3F1frhwamwuKNALfhcjM2/FDQrO5PUVLJIKJw==;EndpointSuffix=core.windows.net");
            CloudBlobClient BlobClient = AzureStorage.CreateCloudBlobClient();
            CloudBlobContainer Container = BlobClient.GetContainerReference("reso-photos");

            CloudBlockBlob BlockBlob = Container.GetBlockBlobReference("test.png"); //the name of the file: we should use somtething like: photo-issueID
            using (var filestream = System.IO.File.OpenRead(@"D:\test.png"))  //delete this
            {
                BlockBlob.UploadFromStream(filestream); //actual upload
            }
        }
    }
}