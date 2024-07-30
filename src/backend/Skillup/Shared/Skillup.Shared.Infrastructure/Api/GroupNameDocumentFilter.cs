using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Skillup.Shared.Infrastructure.Api
{
    internal class GroupNameDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var path in swaggerDoc.Paths)
            {
                var tag = path.Key.Trim('/').Split('/')[0];
                foreach (var operation in path.Value.Operations.Values)
                {
                    var tags = new List<OpenApiTag>
                    {
                        new OpenApiTag { Name = tag }
                    };
                    operation.Tags = tags;
                }
            }
        }
    }
}
