using MediatR;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Modules.Finances.Core.Mappings;
using Skillup.Modules.Finances.Core.Repositories;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Queries
{
    internal class GetItemByIdHandler : IRequestHandler<GetItemByIdRequest, ItemDto>
    {
        private readonly IItemRepository _itemRepository;

        public GetItemByIdHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ItemDto> Handle(GetItemByIdRequest request, CancellationToken cancellationToken)
        {
            var mapper = new ItemMapper();
            var item = await _itemRepository.GetById(request.ItemId) ?? throw new NotFoundException($"Item with ID {request.ItemId} not found");
            return mapper.ItemToDto(item);
        }
    }
}
