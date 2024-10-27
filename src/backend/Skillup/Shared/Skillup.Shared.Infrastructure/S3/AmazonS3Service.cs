using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Shared.Infrastructure.S3
{
    public class AmazonS3Service(IAmazonS3 client, AmazonS3Options options, IWebHostEnvironment environment) : IAmazonS3Service
    {
        private readonly IAmazonS3 _client = client;
        private readonly AmazonS3Options _options = options;
        private readonly IWebHostEnvironment _environment = environment;

        public async Task<PutObjectResponse?> Upload(IFormFile file, string key)
        {
            var putObjectRequest = new PutObjectRequest()
            {
                BucketName = _options.BucketName,
                Key = key,
                ContentType = file.ContentType,
                InputStream = file.OpenReadStream(),
                Metadata =
                {
                    ["x-amz-meta-orginalname"] = file.FileName,
                    ["x-amz-meta-extension"] = Path.GetExtension(file.FileName)
                }
            };

            return await _client.PutObjectAsync(putObjectRequest);
        }

        public async Task<GetObjectResponse> Download(string key)
        {
            var getObjectRequest = new GetObjectRequest()
            {
                BucketName = _options.BucketName,
                Key = key,
            };

            return await _client.GetObjectAsync(getObjectRequest);
        }

        public async Task<string> GetPresignedUrl(string key, double timeToLiveInSeconds)
        {
            var preSignedUrlRequest = new GetPreSignedUrlRequest
            {
                BucketName = _options.BucketName,
                Key = key,
                Expires = DateTime.UtcNow.AddSeconds(timeToLiveInSeconds),
            };

            var url = await client.GetPreSignedURLAsync(preSignedUrlRequest);

            if (_environment.IsDevelopment())
            {
                return url.Replace(options.ServiceURL, "http://localhost");
            }

            return await client.GetPreSignedURLAsync(preSignedUrlRequest);
        }

        public async Task<DeleteObjectResponse?> Delete(string key)
        {
            var deleteObjectRequest = new DeleteObjectRequest()
            {
                BucketName = _options.BucketName,
                Key = key,
            };

            return await _client.DeleteObjectAsync(deleteObjectRequest);
        }
    }
}
