namespace DecisionTech.PriceCalculation
{
    using System.Threading.Tasks;
    using Models;

    public interface ICalculatorService
    {
        Task<Receipt> Calculate(Basket basket);
    }
}
