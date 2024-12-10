using MediatR;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Exceptions;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Modules.Finances.Core.Mappings;
using Skillup.Modules.Finances.Core.Repositories;

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
            var item = await _itemRepository.GetById(request.ItemId) ?? throw new ItemNotFoundException(request.ItemId);
            return mapper.ItemToDto(item);
        }
    }
}
