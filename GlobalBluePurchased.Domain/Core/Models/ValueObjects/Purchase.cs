namespace GlobalBluePurchased.Domain.Core.Models.ValueObjects
{
    public abstract class Purchase : ValueObject
    {
        public abstract decimal? Value { get; }
        public abstract ResultDto Calculate(PurchaseRate purchaseRate);
    }
}