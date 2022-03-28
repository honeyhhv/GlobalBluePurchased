using System.Collections.Generic;

namespace GlobalBluePurchased.Domain.Core.Models.ValueObjects
{
    public class Vat : Purchase
    {
        public override decimal? Value { get; }

        public Vat() : this(null)
        {

        }

        public Vat(decimal? value)
        {
            Value = value;
        }
        public override ResultDto Calculate(PurchaseRate purchaseRate)
        {
            var net = Value * 100 / (decimal)purchaseRate;
            var gross = net + Value;
            var resultDto = new ResultDto()
            {
                Gross = gross,
                Vat = Value,
                Net = net
            };
            return resultDto;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}