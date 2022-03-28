using GlobalBluePurchased.Domain.Core.Models;

namespace GlobalBluePurchased.API.ViewModel
{
    public class CalculatePurchaseData
    {
        public decimal? Net { get; set; }
        public decimal? Vat { get; set; }
        public decimal? Gross { get; set; }
        public PurchaseRate PurchaseRate { get; set; }
    }
}