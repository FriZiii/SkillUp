using MassTransit.Initializers;
using MediatR;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Modules.Finances.Core.Mappings;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Queries
{
    internal class GetItemsByTypeHandler(IItemRepository itemRepository) : IRequestHandler<GetItemsByTypeRequest, IEnumerable<ItemDto>>
    {
        private readonly IItemRepository _itemRepository = itemRepository;

        public async Task<IEnumerable<ItemDto>> Handle(GetItemsByTypeRequest request, CancellationToken cancellationToken)
        {
            var mapper = new ItemMapper();
            var items = await _itemRepository.GetItemsByType(request.Type);
            return items.Select(mapper.ItemToDto);
        }
    }
}
