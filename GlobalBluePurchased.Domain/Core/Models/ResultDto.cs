namespace GlobalBluePurchased.Domain.Core.Models
{
    public class ResultDto
    {
        public decimal? Net { get; set; }
        public decimal? Vat { get; set; }
        public decimal? Gross { get; set; }
    }

    public enum PurchaseRate
    {
        TenPercent = 10,
        ThirteenPercent = 13,
        TwentyPercent = 20,
    }
}