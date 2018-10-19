namespace DecisionTech.PriceCalculation.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Xunit;

    public class DiscountersTests
    {
        [Fact]
        public async Task ButterDiscounter_AppliesDiscount()
        {
            var discounter = new DiscounterService
            {
                Product = "butter",
                QuantityNeeded = 2,
                DiscountOn = "bread",
                Discount = 50,
                DiscountType = DiscountType.Percentage
            };
            var butter = new Product
            {
                Name = "butter",
                Price = new Price { Amount = 0.80m },
                Quantity = 2
            };
            var bread = new Product
            {
                Name = "bread",
                Price = new Price { Amount = 1.00m },
                Quantity = 2
            };
            var products = new List<Product>
            {
                butter,
                bread
            };
            var receipt = new Receipt
            {
                Products = products,
                Total = products.Sum(p => p.Quantity * p.Price.Amount)
            };

            await discounter.ApplyDiscountAsync(receipt);

            Assert.Equal(3.10m, receipt.Total);
        }

        [Fact]
        public async Task MilkDiscounter_AppliesDiscount()
        {
            var discounter = new DiscounterService
            {
                Product = "milk",
                QuantityNeeded = 4,
                DiscountOn = "milk",
                Discount = 100,
                DiscountType = DiscountType.Percentage
            };
            var milk = new Product
            {
                Name = "milk",
                Price = new Price { Amount = 1.15m },
                Quantity = 8
            };

            var butter = new Product
            {
                Name = "butter",
                Price = new Price { Amount = 0.80m },
                Quantity = 2
            };

            var products = new List<Product> { milk,butter };
            var receipt = new Receipt
            {
                Products = products,
                Total = products.Sum(p => p.Quantity * p.Price.Amount)
            };

            await discounter.ApplyDiscountAsync(receipt);

            Assert.Equal(8.50m, receipt.Total);
        }
    }
}
