using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Repositories;
using Skillup.Modules.Finances.Core.ValueObjects;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Commannds
{
    internal class EditItemPriceHandler : IRequestHandler<EditItemPriceRequest>
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<EditItemPriceHandler> _logger;

        public EditItemPriceHandler(IItemRepository itemRepository, ILogger<EditItemPriceHandler> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
        }

        public async Task Handle(EditItemPriceRequest request, CancellationToken cancellationToken)
        {
            var itemToEdit = await _itemRepository.GetById(request.ItemId);

            if (!itemToEdit.AuthorId.Equals(request.UserId)) throw new UnauthorizedException("Only author of item can change price");

            var currency = new Currency(request.Currency);
            await _itemRepository.Edit(request.ItemId, currency);
            _logger.LogInformation("Price edited");
        }
    }
}
