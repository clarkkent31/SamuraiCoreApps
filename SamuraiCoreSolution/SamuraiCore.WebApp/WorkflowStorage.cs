using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamuraiCore.WebApp
{
    public class WorkflowStorage
        
    {
        protected readonly CloudQueue QueueClient;
        protected readonly CloudBlobContainer _blobContainer;

        #region Ctor

        /// <summary>
        /// <para>The queue reference must be all lowercase!</para>
        /// </summary>
        public WorkflowStorage(string connectionString, string queueReference, bool isBlob = false)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            if (isBlob)
            {
                var blobClient = storageAccount.CreateCloudBlobClient();
                _blobContainer = blobClient.GetContainerReference(queueReference);
                _blobContainer.CreateIfNotExists();
            }
            else
            {
                var queueClient = storageAccount.CreateCloudQueueClient();
                QueueClient = queueClient.GetQueueReference(queueReference);
                QueueClient.CreateIfNotExists();
            }
        }

        public void AddFileStreamToAzureBlob(string fileNameToStore, Stream inputStream, string blobContainer)
        {
            try
            {
                // Retrieve storage account from connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Config.AzureStorageConnection);

                // Create the blob client.
                var blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve a reference to a container.
                var container = blobClient.GetContainerReference(blobContainer);

                // Create the container if it doesn't already exist.
                container.CreateIfNotExists();

                // Retrieve reference to a blob named "myblob".
                var blockBlob = container.GetBlockBlobReference(fileNameToStore);

                // Upload the file
                blockBlob.UploadFromStream(inputStream);

                new BaseLoggerBuilderComponentSuccess(null, -1, SystemEnum.WorkflowProcessing)
                    .InProcess($"AddFileStreamToAzureBlob() : Successfully uploaded the file stream to {blobContainer} blob at - { DateTime.Now}").InSystemCategory(CategoryEnum.WorkflowStorageComponent)
                    .MustWaitForLogQueueEmpty(true).WithLogLevel(LogLevel.Info).WriteToLog("File successfully uploaded");
            }
            catch (Exception ex)
            {
                new BaseLoggerBuilderComponentFailed("N/A", -1, SystemEnum.WorkflowProcessing)
                    .InProcess($"AddFileStreamToAzureBlob() : Failed while uploading file stream to {blobContainer} blob at - { DateTime.Now}").InSystemCategory(CategoryEnum.WorkflowStorageComponent)
                    .MustWaitForLogQueueEmpty(true).WithLogLevel(LogLevel.Error).WriteToLog(ex.GetBaseException());


            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieve an item from the queue
        /// </summary>
        /// <param name="token">Cancellation Token</param>
        /// <returns>The message from the queue</returns>
        public Task<CloudQueueMessage> GetItemFromQueue(CancellationToken token)
        {
            return QueueClient.GetMessageAsync(token);
        }

        public Task<CloudQueueMessage> GetItemFromQueue(CancellationToken token, TimeSpan visibilityTimeout)
        {
            return QueueClient.GetMessageAsync(visibilityTimeout, new QueueRequestOptions(), new OperationContext(), token);
        }

        /// <summary>
        /// Add an message to the queue and assign an identifier
        /// </summary>
        /// <param name="id">An identifier for the queue</param>
        public void AddItemToQueue(string id)
        {
            QueueClient.AddMessage(new CloudQueueMessage(id));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Add an message to the queue and assign an identifier. </summary>
        ///
        /// <remarks>   Marcel Verbiest, 2018/01/30. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="item"> The item. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void AddItemToQueue<T>(T item)
        {
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(item);

            QueueClient.AddMessage(new CloudQueueMessage(serializedResult));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Add an message to the queue and assign an identifier. </summary>
        ///
        /// <remarks>   Dpjain, 12/07/2018. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="item">             The item. </param>
        /// <param name="visibilityTime">   (Optional) The visibility time. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void AddItemToQueue<T>(T item, TimeSpan? visibilityTime = null)
        {
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(item);
            CloudQueueMessage message = new CloudQueueMessage(serializedResult);

            QueueClient.AddMessage(message, null, visibilityTime);
        }

        /// <summary>
        /// Remove an message from the queue
        /// </summary>
        /// <param name="message">The message to delete</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns></returns>
        public Task DeleteItemFromQueue(CloudQueueMessage message, CancellationToken token)
        {
            return QueueClient.DeleteMessageAsync(message, token);
        }

        /// <summary>
        /// Ckear the queue
        /// </summary>
        public void ClearQueue()
        {
            QueueClient.Clear();
        }

        /// <summary>
        /// Retrieve blob items from the queue
        /// </summary>
        /// <param name="blobName"></param>
        /// <returns></returns>
        public CloudBlockBlob GetItemsFromBlob(string blobName)
        {
            return _blobContainer.GetBlockBlobReference(blobName);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates BLOB file. </summary>
        ///
        /// <remarks>   Sachawla, 10/23/2017. </remarks>
        ///
        /// <param name="containerName">    Name of the container. </param>
        /// <param name="fileName">         Filename of the file. </param>
        /// <param name="contentType">      Type of the content. </param>
        /// <param name="fileData">         Information describing the file. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool CreateBlobFile(string fileName, string contentType, byte[] fileData)
        {
            CloudBlockBlob blockBlob = _blobContainer.GetBlockBlobReference(fileName);
            blockBlob.Properties.ContentType = contentType;
            // blockBlob.UploadFromStream(fileData);
            // Create or overwrite the "fileName" blob with the contents of a local file
            // named “myfile”.
            using (var memoryStream = new MemoryStream(fileData))
            {
                blockBlob.UploadFromStream(memoryStream);
            }
            return true;
        }

        #endregion
    }

}
