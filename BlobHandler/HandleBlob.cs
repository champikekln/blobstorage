using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;
using BlobHandler;

namespace Blob
{
    public class HandleBlob
    {
        private string _connectionString { get; set; }
        private string _containerName { get; set; }
        private string _blobName { get; set; }
        private FileStream _fileStream { get; set; }

        public HandleBlob(string connectionstring, string container, string blobName, FileStream fileStream)
        {
            _blobName = blobName;
            _connectionString = connectionstring;
            _containerName = container;
            _connectionString = connectionstring;
            _fileStream = fileStream;
        }
       
        public async Task<Uri> UploadFileToBlob()
        {
            Uri? blobUri = null;
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            await containerClient.CreateIfNotExistsAsync();
            BlockBlobClient blockBlobClient = containerClient.GetBlockBlobClient(_blobName);

            using (FileStream fs = _fileStream)
            {
                long fileSize = fs.Length;
                int blockSize = 1 * 1024 * 1024;

                List<Task> uploadTasks = new List<Task>();
                int blockCount = (int)Math.Ceiling((double)fileSize / blockSize);
                byte[] buffer = new byte[blockSize];

                for (int i = 0; i < blockCount; i++)
                {
                    int bytesRead = await fs.ReadAsync(buffer, 0, blockSize);
                    byte[] blockData = buffer.Take(bytesRead).ToArray();
                    string blockId = Convert.ToBase64String(BitConverter.GetBytes(i)); 
                    uploadTasks.Add(blockBlobClient.StageBlockAsync(blockId, new MemoryStream(blockData)));
                }
                await Task.WhenAll(uploadTasks);
                List<string> blockIds = uploadTasks.Select(task => Convert.ToBase64String(BitConverter.GetBytes(uploadTasks.IndexOf(task)))).ToList();
                await blockBlobClient.CommitBlockListAsync(blockIds);
                 blobUri = blockBlobClient.Uri;
            }
            return blobUri;
        }

        public async Task<List<Blobs>> LoadAllBlobs()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            List<Blobs> blobs = new List<Blobs>();

            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                Blobs blob = new Blobs
                {
                    name = blobItem.Name,
                    blobUrl = containerClient.GetBlobClient(blobItem.Name).Uri.ToString(),
                    dateModified = blobItem.Properties.LastModified?.DateTime ?? DateTime.MinValue
                };
                blobs.Add(blob);
            }
            return blobs;
        }
    }
}
