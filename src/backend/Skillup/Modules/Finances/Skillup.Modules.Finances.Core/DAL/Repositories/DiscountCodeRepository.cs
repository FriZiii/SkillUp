using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.DAL.Repositories
{
    internal class DiscountCodeRepository(FinancesDbContext context) : IDiscountCodeRepository
    {
        private readonly FinancesDbContext context = context;
        private readonly DbSet<DiscountCode> _discountCodes = context.DiscountCodes;
        private readonly DbSet<DiscountedItem> _discountedItems = context.DiscountedItems;

        public async Task Add(DiscountCode discountCode)
        {
            try
            {
                await _discountCodes.AddAsync(discountCode);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException is Npgsql.PostgresException exception)
                {
                    if (exception.SqlState == Npgsql.PostgresErrorCodes.UniqueViolation)
                    {
                        throw new Exception("This code already exist, is not unique"); // TODO: Custom ex "This code already exist, is not unique"
                    }
                }
                throw new Exception(); // smth went wrong
            }
        }

        public async Task Update(DiscountCode discountCode)
        {
            var discountCodeToEdit = await _discountCodes.Include(x => x.DiscountedItems)
                .FirstOrDefaultAsync(x => x.Id == discountCode.Id) ?? throw new Exception(); // TODO: Custom Ex

            if (!discountCodeToEdit.AppliesToEntireCart && discountCode.AppliesToEntireCart)
            {
                _discountedItems.RemoveRange(discountCodeToEdit.DiscountedItems);
            }

            discountCodeToEdit.Code = discountCode.Code;
            discountCodeToEdit.DiscountValue = discountCode.DiscountValue;

            discountCodeToEdit.IsActive = discountCode.IsActive;
            discountCodeToEdit.IsPublic = discountCode.IsPublic;

            discountCodeToEdit.DiscountValue = discountCode.DiscountValue;

            discountCodeToEdit.AppliesToEntireCart = discountCode.AppliesToEntireCart;

            await context.SaveChangesAsync();
        }

        public async Task DeleteById(Guid discountCodeId)
        {
            var discountCodeToDelete = await _discountCodes.FirstOrDefaultAsync(x => x.Id == discountCodeId) ?? throw new Exception();
            _discountCodes.Remove(discountCodeToDelete);
            await context.SaveChangesAsync();
        }

        public async Task<DiscountCode?> GetById(Guid discountCodeId)
            => await _discountCodes
                .Include(x => x.DiscountedItems)
                    .ThenInclude(x => x.Item)
                .FirstOrDefaultAsync(x => x.Id == discountCodeId);

        public async Task<IEnumerable<DiscountCode>> GetByOwner(Guid ownerId)
        {
            var codes = await _discountCodes.Include(x => x.DiscountedItems)
                    .ThenInclude(x => x.Item).Where(c => c.OwnerId == ownerId).ToListAsync();
            return codes;
        }

        public async Task<IEnumerable<DiscountCode>> GetPublic()
            => await _discountCodes.Where(x => x.IsPublic)
                .Include(x => x.DiscountedItems)
                    .ThenInclude(x => x.Item)
                .ToListAsync();

        public async Task ToggleDiscountCodeForItem(Guid discountCodeId, Guid itemId)
        {
            var discountedItem = await _discountedItems
                .FirstOrDefaultAsync(x => x.ItemId == itemId && x.DiscountCodeId == discountCodeId);

            if (discountedItem != null)
                _discountedItems.Remove(discountedItem);
            else
                await _discountedItems.AddAsync(new DiscountedItem(discountCodeId, itemId));

            await context.SaveChangesAsync();
        }

        public async Task<DiscountCode?> GetByCode(string code)
            => await _discountCodes
                .Include(x => x.DiscountedItems)
                    .ThenInclude(x => x.Item)
                .FirstOrDefaultAsync(x => x.Code == code);
    }
}
