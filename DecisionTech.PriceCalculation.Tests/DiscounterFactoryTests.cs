namespace DecisionTech.PriceCalculation.Tests
{
    using Models;
    using Xunit;

    public class DiscounterFactoryTests
    {
        [Fact]
        public void DiscounterFactory_GetsDiscounterForButter()
        {
            var factory = new DiscounterFactory();
            var product = new Product { Name = "butter" };

            var discounter = factory.GetDiscounter(product);

            Assert.Equal("butter", discounter.Product);
            Assert.Equal(2,discounter.QuantityNeeded);
            Assert.Equal("bread",discounter.DiscountOn);
            Assert.Equal(50,discounter.Discount);
            Assert.Equal(DiscountType.Percentage,discounter.DiscountType);
        }

        [Fact]
        public void DiscounterFactory_GetsDiscounterForMilk()
        {
            var factory = new DiscounterFactory();
            var product = new Product { Name = "milk" };

            var discounter = factory.GetDiscounter(product);

            Assert.Equal( "milk", discounter.Product);
            Assert.Equal(4,discounter.QuantityNeeded);
            Assert.Equal("milk",discounter.DiscountOn);
            Assert.Equal(100, discounter.Discount);
            Assert.Equal(DiscountType.Percentage,discounter.DiscountType);
        }
    }
}
