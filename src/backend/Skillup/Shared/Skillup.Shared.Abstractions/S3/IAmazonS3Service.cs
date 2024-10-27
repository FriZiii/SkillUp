using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;

namespace Skillup.Shared.Abstractions.S3
{
    public interface IAmazonS3Service
    {
        Task<PutObjectResponse?> Upload(IFormFile file, string key);
        Task<GetObjectResponse> Download(string key);
        Task<string> GetPresignedUrl(string key, double timeToLiveInSeconds);
        Task<DeleteObjectResponse?> Delete(string key);
    }
}
