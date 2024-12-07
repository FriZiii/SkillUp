using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Skillup.Shared.Abstractions.S3;
using Skillup.Shared.Abstractions.Time;
using System.Text;

namespace Skillup.Shared.Infrastructure.S3
{
    public class AmazonS3Service(IAmazonS3 client, AmazonS3Options options, IWebHostEnvironment environment, IClock clock, ILogger<AmazonS3Service> logger) : IAmazonS3Service
    {
        private readonly IAmazonS3 _client = client;
        private readonly AmazonS3Options _options = options;
        private readonly IWebHostEnvironment _environment = environment;
        private readonly IClock _clock = clock;
        private readonly ILogger<AmazonS3Service> _logger = logger;

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
                    ["x-amz-meta-orginalname"] = Convert.ToBase64String(Encoding.UTF8.GetBytes(file.FileName)),
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

        public async Task<string> GetPresignedUrl(string key, double timeToLiveInSeconds = 604_800)
        {
            var preSignedUrlRequest = new GetPreSignedUrlRequest
            {
                BucketName = _options.BucketName,
                Key = key,
                Expires = DateTime.UtcNow.AddSeconds(timeToLiveInSeconds),
            };

            var url = await _client.GetPreSignedURLAsync(preSignedUrlRequest);

            if (_environment.IsDevelopment())
            {
                return url.Replace(_options.ServiceURL.Replace("http://", "https://"), "http://localhost");
            }

            return await _client.GetPreSignedURLAsync(preSignedUrlRequest);
        }

        public string GetPulicUrl(string key)
        {
            if (_environment.IsDevelopment())
            {
                return $"http://localhost:{_options.Port}/{_options.BucketName}/{key}?timestamp={_clock.CurrentDate()}";
            }
            else
            {
                return $"https://{_options.BucketName}.s3.{_options.Region}.amazonaws.com/{key}?timestamp={_clock.CurrentDate()}";
            }
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
