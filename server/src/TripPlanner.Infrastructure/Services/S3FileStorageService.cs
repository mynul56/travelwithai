using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using TripPlanner.Application.Interfaces;

namespace TripPlanner.Infrastructure.Services
{
    public class S3FileStorageService : IFileStorageService
    {
        private readonly IConfiguration _config;

        public S3FileStorageService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> UploadPdfAsync(byte[] pdfBytes, string fileName)
        {
            var accessKey = _config["AWS:AccessKey"];
            var secretKey = _config["AWS:SecretKey"];
            var bucketName = _config["AWS:BucketName"] ?? "trip-planner-pdfs";
            var region = Amazon.RegionEndpoint.USEast1;

            if (string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(secretKey))
            {
                // Mock behavior for local development if AWS keys are not configured
                return $"https://mock-s3-bucket.com/{bucketName}/{fileName}";
            }

            using var client = new AmazonS3Client(accessKey, secretKey, region);
            using var newMemoryStream = new MemoryStream(pdfBytes);

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = newMemoryStream,
                Key = fileName,
                BucketName = bucketName,
                ContentType = "application/pdf"
            };

            var fileTransferUtility = new TransferUtility(client);
            await fileTransferUtility.UploadAsync(uploadRequest);

            return $"https://{bucketName}.s3.amazonaws.com/{fileName}";
        }
    }
}
