using MediatR;
using Skillup.Modules.Finances.Core.Exceptions;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Repositories;
using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Commannds
{
    internal class EditItemPriceHandler : IRequestHandler<EditItemPriceRequest>
    {
        private readonly IItemRepository _itemRepository;

        public EditItemPriceHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task Handle(EditItemPriceRequest request, CancellationToken cancellationToken)
        {
            var itemToEdit = await _itemRepository.GetById(request.ItemId);

            if (!itemToEdit.AuthorId.Equals(request.UserId)) throw new OnlyAuthorCanChangePriceException(itemToEdit.Type);

            var currency = new Currency(request.Currency);
            await _itemRepository.Edit(request.ItemId, currency);
        }
    }
}
