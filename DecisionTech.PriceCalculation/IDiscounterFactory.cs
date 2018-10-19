namespace DecisionTech.PriceCalculation
{
    using Models;

    public interface IDiscounterFactory
    {
        IDiscounterService GetDiscounter(Product product);
    }
}
