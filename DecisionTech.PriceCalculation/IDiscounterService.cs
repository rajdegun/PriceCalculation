namespace DecisionTech.PriceCalculation
{
    using System.Threading.Tasks;
    using Models;

    public interface IDiscounterService
    {
        string Product { get; }

        int QuantityNeeded { get; }

        string DiscountOn { get; }

        decimal Discount { get; }

        DiscountType DiscountType { get; }

        bool QualifiesForDiscount(Product product, Product productWithDiscount);

        Task ApplyDiscountAsync(Receipt receipt);
    }
}
