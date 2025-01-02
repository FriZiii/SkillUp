using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Tests;

public class DiscountCodeTests
{
    [Fact]
    public void ApplyDiscountCode_PercentageDiscountCode_AppliesDiscountToEntireCart()
    {
        // Arrange
        var cart = new Cart
        {
            Id = Guid.NewGuid(),
            Total = new Currency(0),
            Items =
            [
                new() {
                    Id = Guid.NewGuid(),
                    Item = new Item(Guid.NewGuid(),Guid.NewGuid(), ItemType.Course, 100),
                    Price = 100m
                },
                new() {
                    Id = Guid.NewGuid(),
                    Item = new Item(Guid.NewGuid(),Guid.NewGuid(), ItemType.Course, 50),
                    Price = 50m
                }
            ]
        };

        var discountCode = new PercentageDiscountCode(new AddDiscountCodeDto
        {
            Id = Guid.NewGuid(),
            OwnerId = Guid.NewGuid(),
            Code = "PercentageTest",
            Type = DiscountCodeType.Percentage,
            IsActive = true,
            IsPublic = false,
            StartAt = DateTime.UtcNow,
            ExpireAt = DateTime.UtcNow.AddDays(1),
            DiscountValue = 10m,
            AppliesToEntireCart = true
        });

        // Act
        cart.ApplyDiscountCode(discountCode);

        // Assert
        Assert.Equal(135m, cart.Total.Amount);
    }

    [Fact]
    public void ApplyDiscountCode_PercentageDiscountCode_AppliesDiscountToDiscountedItemsInCart()
    {
        // Arrange
        var discountedItem = new Item(Guid.NewGuid(), Guid.NewGuid(), ItemType.Course, 100);

        var cart = new Cart
        {
            Id = Guid.NewGuid(),
            Total = new Currency(0),
            Items =
            [
                new() {
                    Id = Guid.NewGuid(),
                    Item = discountedItem,
                    ItemId = discountedItem.Id,
                    Price = 100m
                },
                new() {
                    Id = Guid.NewGuid(),
                    Item = new Item(Guid.NewGuid(),Guid.NewGuid(), ItemType.Course, 50),
                    Price = 50m
                }
            ]
        };

        var discountCode = new PercentageDiscountCode(new AddDiscountCodeDto
        {
            Id = Guid.NewGuid(),
            OwnerId = Guid.NewGuid(),
            Code = "PercentageTest",
            Type = DiscountCodeType.Percentage,
            IsActive = true,
            IsPublic = false,
            StartAt = DateTime.UtcNow,
            ExpireAt = DateTime.UtcNow.AddDays(1),
            DiscountValue = 10m,
            AppliesToEntireCart = false,
        });
        discountCode.DiscountedItems = [new DiscountedItem(discountCode.Id, discountedItem.Id)];

        // Act
        cart.ApplyDiscountCode(discountCode);

        // Assert
        Assert.Equal(140m, cart.Total.Amount);
    }

    [Fact]
    public void ApplyDiscountCode_FixedAmountDiscountCode_AppliesDiscountToEntireCart()
    {
        // Arrange
        List<CartItem> items =
        [
            new() {
                Id = Guid.NewGuid(),
                Item = new Item(Guid.NewGuid(),Guid.NewGuid(), ItemType.Course, 100),
                Price = 100m
            },
            new() {
                Id = Guid.NewGuid(),
                Item = new Item(Guid.NewGuid(),Guid.NewGuid(), ItemType.Course, 50),
                Price = 50m
            }
        ];

        var cart = new Cart
        {
            Id = Guid.NewGuid(),
            Total = items.Sum(x => x.Price),
            Items = items
        };

        var discountCode = new FixedAmountDiscountCode(new AddDiscountCodeDto
        {
            Id = Guid.NewGuid(),
            OwnerId = Guid.NewGuid(),
            Code = "FixedAmountTest",
            Type = DiscountCodeType.FixedAmount,
            IsActive = true,
            IsPublic = false,
            StartAt = DateTime.UtcNow,
            ExpireAt = DateTime.UtcNow.AddDays(1),
            DiscountValue = 100m,
            AppliesToEntireCart = true,
        });

        // Act
        cart.ApplyDiscountCode(discountCode);

        // Assert
        Assert.Equal(50m, cart.Total.Amount);
    }
}