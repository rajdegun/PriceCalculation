namespace DecisionTech.PriceCalculation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;

    public class CalculatorService : ICalculatorService
    {
        private readonly IDiscounterFactory _discounterFactory;

        public CalculatorService(IDiscounterFactory discounterFactory)
        {
            _discounterFactory = discounterFactory;
        }

        public async Task<Receipt> Calculate(Basket basket)
        {
            var receipt = new Receipt();

            receipt.Products = basket.Products;
            receipt.Total = receipt
                .Products
                .Sum(p => p.Price.Amount * p.Quantity);

            foreach (var product in receipt.Products)
            {
                var discounter = _discounterFactory.GetDiscounter(product);
                if (discounter != null)
                {
                    await discounter
                        .ApplyDiscountAsync(receipt)
                        .ConfigureAwait(false);
                }
            }

            return receipt;
        }
    }
}
