using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Services
{
    public class S3StorageService : IS3StorageService
    {
        private readonly IConfiguration _configuration;

        public S3StorageService(IConfiguration configuration
            )
        {
            _configuration = configuration;
        }

        private AmazonS3Client CreateConnection()
        {
            var accessKeyID = _configuration["AWS:AccessKeyID"];
            var secretAccessKey = _configuration["AWS:SecretAccessKey"];
            var region = _configuration["AWS:Region"];

            var awsCredentials = new BasicAWSCredentials(accessKeyID, secretAccessKey);
            var amazonS3Config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(region)
            };

            var amazonS3Client = new AmazonS3Client(awsCredentials, amazonS3Config);
            return amazonS3Client;
        }

        public async Task<string> GetUrlFile(string fileName)
        {
            AmazonS3Client amazonS3Client = null;
            try
            {
                amazonS3Client = CreateConnection();

                var request = new GetPreSignedUrlRequest
                {
                    BucketName = _configuration["AWS:BucketName"],
                    Key = fileName,
                    Expires = DateTime.Now.AddMinutes(5)
                };

                string path = await amazonS3Client.GetPreSignedURLAsync(request);

                return path;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> UploadFileToS3(IFormFile file)
        {
            AmazonS3Client amazonS3Client = null;
            try
            {
                amazonS3Client = CreateConnection();
                var newMemoryStream = new MemoryStream();

                file.CopyTo(newMemoryStream);

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = newMemoryStream,
                    Key = file.FileName,
                    BucketName = _configuration["AWS:BucketName"],
                    ContentType = file.ContentType
                };

                var fileTransferUtility = new TransferUtility(amazonS3Client);

                await fileTransferUtility.UploadAsync(uploadRequest);

                return await GetUrlFile(file.FileName);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
