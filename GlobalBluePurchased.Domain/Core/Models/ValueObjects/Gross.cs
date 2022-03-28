using System;
using System.Collections.Generic;

namespace GlobalBluePurchased.Domain.Core.Models.ValueObjects
{
    public class Gross : Purchase
    {
        public override decimal? Value { get; }

        public Gross() : this(null)
        {

        }

        public Gross(decimal? value)
        {
            Value = value;
        }
        public override ResultDto Calculate(PurchaseRate purchaseRate)
        {
            var vatRate = (decimal)purchaseRate / 100;
            var n = Value / (vatRate + 1);
            var net = Math.Round(n.GetValueOrDefault(), 2);
            var v = Value * vatRate / (vatRate + 1);
            var vat = Math.Round(v.GetValueOrDefault(), 2);
            var resultDto = new ResultDto
            {
                Gross = Value,
                Vat = vat,
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