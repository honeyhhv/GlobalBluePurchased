using System.Collections.Generic;

namespace GlobalBluePurchased.Domain.Core.Models.ValueObjects
{
    public class Net : Purchase
    {
        public override decimal? Value { get; }

        public Net() : this(null)
        {

        }
        public Net(decimal? value)
        {
            Value = value;
        }
        public override ResultDto Calculate(PurchaseRate purchaseRate)
        {
            var vat = Value * (decimal)purchaseRate / 100;
            var gross = Value + vat;
            var resultDto = new ResultDto
            {
                Gross = gross,
                Vat = vat,
                Net = Value
            };
            return resultDto;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

    }
}