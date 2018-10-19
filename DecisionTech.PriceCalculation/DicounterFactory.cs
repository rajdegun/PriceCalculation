namespace DecisionTech.PriceCalculation
{
    using System;
    using System.Collections.Concurrent;
    using Models;

    public class DiscounterFactory : IDiscounterFactory
    {       
        public IDiscounterService GetDiscounter(Product product)
        {
            IDiscounterService discounter = null;
               switch (product.Name)
                {
                    case "milk":
                        {
                            discounter = new DiscounterService
                            {
                                Product = "milk",
                                QuantityNeeded = 4,
                                DiscountOn = "milk",
                                Discount = 100,
                                DiscountType = DiscountType.Percentage
                            };
                            break;
                        }
                    case "butter":
                        {
                            discounter = new DiscounterService
                            {
                                Product = "butter",
                                QuantityNeeded = 2,
                                DiscountOn = "bread",
                                Discount = 50,
                                DiscountType = DiscountType.Percentage
                            };
                            break;
                        }
                    default:
                        break;
                }
         
            return discounter;
        }
    }
}
