using MediatR;

namespace Skillup.Modules.Finances.Core.Features.Requests.Commannds
{
    internal record AddCartItemRequest : IRequest<Guid>
    {
        public AddCartItemRequest(Guid? cartId, Guid itemId)
        {
            ItemId = itemId;

            if (cartId == null)
                CartId = Guid.NewGuid();
            else
                CartId = (Guid)cartId;
        }

        public Guid ItemId { get; set; }
        public Guid CartId { get; set; }
    }
}
