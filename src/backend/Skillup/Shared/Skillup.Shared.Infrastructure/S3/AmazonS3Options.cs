using Skillup.Shared.Abstractions.Options;
using Skillup.Shared.Infrastructure.EnvironmentInjector;

namespace Skillup.Shared.Infrastructure.S3
{
    public class AmazonS3Options : IOption
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string Region { get; set; }
        public string BucketName { get; set; }

        [EnvironmentVariable("SERVICE_URL")]
        public string ServiceURL { get; set; }
    }
}
