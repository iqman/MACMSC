using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace S1CloudServices
{
    public static class AzureDrive
    {
        private static CloudDrive myCloudDrive;

        private const string connectionString = "DefaultEndpointsProtocol=http;AccountName=macmsc;AccountKey=5ZRVGC2QFSCAUh6SS8mjf1oF1T0I4oZM9vhPKs1bJ4OF7UV8e58r+49XKORwmTG9aLrU0ZNxf6nET7j+5NsRiQ==";

        public static string Mount()
        {
            CloudStorageAccount storageAccount;
            if (RoleEnvironment.IsEmulated)
            {
                storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            }
            else
            {
                storageAccount = CloudStorageAccount.Parse(connectionString);   
            }

            LocalResource localCache = RoleEnvironment.GetLocalResource("InstanceDriveCache");
            CloudDrive.InitializeCache(localCache.RootPath, localCache.MaximumSizeInMegabytes);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            blobClient.GetContainerReference("drives").CreateIfNotExist();

            myCloudDrive = storageAccount.CreateCloudDrive(
                blobClient
                    .GetContainerReference("drives")
                    .GetPageBlobReference("mysupercooldrive.vhd")
                    .Uri.ToString()
                );

            myCloudDrive.CreateIfNotExist(64);

            return myCloudDrive.Mount(25, DriveMountOptions.None);
        }

        public static void UnMount()
        {
            myCloudDrive.Unmount();
        }
    }
}
