namespace DecisionTech.PriceCalculation.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Moq;
    using Xunit;
    
    public class CalculatorTests
    {
        [Fact]
        public async void Calculator_CalculatesPrices()
        {
            var discounterFactory = new Mock<IDiscounterFactory>();
            var calculator = new CalculatorService(discounterFactory.Object);
            var products = new List<Product>()
            {
                new Product { Name = "bread", Quantity = 1, Price = new Price { Amount = 1.00m }},
                new Product { Name = "butter", Quantity = 1, Price = new Price { Amount =0.8m }},
                new Product { Name = "milk", Quantity = 1, Price = new Price { Amount = 1.15m }}
            };
            var basket = new Basket
            {
                Products = products
            };
            discounterFactory
                .Setup(f => f.GetDiscounter(It.IsAny<Product>()))
                .Returns<IDiscounterService>(null);

            var receipt = await calculator.Calculate(basket);

            Assert.True(receipt.Total == 2.95m);
        }

        [Fact]
        public async void Calculator_CalculatesPricesWithDiscount()
        {
            var butterDiscounter = new Mock<IDiscounterService>();
            var discounterFactory = new Mock<IDiscounterFactory>();
            var calculator = new CalculatorService(discounterFactory.Object);
            var butter = new Product { Name = "butter", Quantity = 2, Price = new Price { Amount = 0.80m } };
            var bread = new Product { Name = "bread", Quantity = 1, Price = new Price { Amount = 1.00m } };
            var products = new List<Product>()
            {
                butter,
                bread
            };
            var basket = new Basket
            {
                Products = products
            };
            discounterFactory
                .Setup(f => f.GetDiscounter(It.Is<Product>(p => p.Name == "butter")))
                .Returns(butterDiscounter.Object);
            butterDiscounter
                .Setup(d => d.ApplyDiscountAsync(It.IsAny<Receipt>()))
                .Callback((Receipt r) =>
                {
                    r.Total -= bread.Price.Amount / 2;
                })
                .Returns(Task.Delay(0));

            var receipt = await calculator.Calculate(basket);

            Assert.Equal(2.10m, receipt.Total);
        }
    }
}
