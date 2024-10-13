using MediatR;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Exceptions;
using Skillup.Modules.Finances.Core.Features.Requests;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers
{
    internal class GetItemByIdHandler : IRequestHandler<GetItemByIdRequest, Item>
    {
        private readonly IItemRepository _itemRepository;

        public GetItemByIdHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<Item> Handle(GetItemByIdRequest request, CancellationToken cancellationToken)
        {
            var item = await _itemRepository.GetById(request.ItemId) ?? throw new ItemNotFoundException(request.ItemId);
            return item;
        }
    }
}
