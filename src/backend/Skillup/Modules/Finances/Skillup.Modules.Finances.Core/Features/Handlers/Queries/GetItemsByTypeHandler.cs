using MediatR;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Queries
{
    internal class GetItemsByTypeHandler : IRequestHandler<GetItemsByTypeRequest, IEnumerable<Item>>
    {
        private readonly IItemRepository _itemRepository;

        public GetItemsByTypeHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public Task<IEnumerable<Item>> Handle(GetItemsByTypeRequest request, CancellationToken cancellationToken)
        {
            var items = _itemRepository.GetItemsByType(request.Type);
            return items;
        }
    }
}
