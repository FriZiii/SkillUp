using MediatR;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Finances.Core.Features.Requests
{
    internal record EditItemPriceRequest(decimal Currency) : IRequest
    {
        [JsonIgnore]
        public Guid ItemId { get; set; }

        [JsonIgnore]
        public Guid UserId { get; internal set; }
    };
}
