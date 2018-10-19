namespace DecisionTech.PriceCalculation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;

    public class DiscounterService : IDiscounterService
    {
        public string Product { get; set; }

        public int QuantityNeeded { get; set; }

        public string DiscountOn { get; set; }

        public decimal Discount { get; set; }

        public DiscountType DiscountType { get; set; }

        public async Task ApplyDiscountAsync(Receipt receipt)
        {
            await Task.Run(() =>
            {
                var product = receipt
                    .Products
                    .FirstOrDefault(r => r.Name == Product);
                var productWithDiscount = receipt
                    .Products
                    .FirstOrDefault(r => r.Name == DiscountOn);

                if (QualifiesForDiscount(product, productWithDiscount))
                {
                    ModifyTotal(product, productWithDiscount, receipt);
                }
            });
        }

        public bool QualifiesForDiscount(Product product, Product productWithDiscount)
        {
            return product != null
                && productWithDiscount != null
                && product.Quantity >= QuantityNeeded;
        }

        private void ModifyTotal(Product product, Product productWithDiscount, Receipt receipt)
        {
            if (DiscountType == DiscountType.Percentage)
            {
                var numberOfDiscountItems = Math.Floor((decimal)product.Quantity / QuantityNeeded);
                numberOfDiscountItems = productWithDiscount.Quantity > numberOfDiscountItems ? numberOfDiscountItems : productWithDiscount.Quantity;
                receipt.Total -= numberOfDiscountItems * productWithDiscount.Price.Amount * Discount / 100;
            }
        }
    }
}
