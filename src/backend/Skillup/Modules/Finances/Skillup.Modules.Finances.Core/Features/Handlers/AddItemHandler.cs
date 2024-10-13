using MediatR;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Features.Requests;
using Skillup.Modules.Finances.Core.Repositories;
using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.Features.Handlers
{
    internal class AddItemHandler : IRequestHandler<AddItemRequest>
    {
        private readonly IItemRepository _itemRepository;

        public AddItemHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task Handle(AddItemRequest request, CancellationToken cancellationToken)
        {
            await _itemRepository.Add(new Item(request.ItemId, request.AuthorId, request.Type, new Currency(0)));
        }
    }
}
