using MediatR;

namespace Skillup.Modules.Finances.Core.Features.Requests.Commannds
{
    internal record AddCartItemRequest : IRequest
    {
        public AddCartItemRequest(Guid? cartId, Guid itemId)
        {
            ItemId = itemId;
            CartId = cartId;
        }

        public Guid ItemId { get; set; }
        public Guid? CartId { get; set; }
    }
}
