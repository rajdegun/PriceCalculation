namespace DecisionTech.PriceCalculation.Models
{
    using System.Collections.Generic;

    public class Receipt
    {
        public decimal Total { get; set; }
        public IList<Product> Products { get; set; }
    }
}
