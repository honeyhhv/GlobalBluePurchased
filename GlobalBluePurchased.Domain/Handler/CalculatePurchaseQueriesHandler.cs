using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GlobalBluePurchased.Domain.Core.Models;
using GlobalBluePurchased.Domain.Core.Models.ValueObjects;
using GlobalBluePurchased.Domain.Request;
using MediatR;

namespace GlobalBluePurchased.Domain.Handler
{
    public class CalculatePurchaseQueriesHandler : IRequestHandler<CalculatePurchaseQueries, ResultDto>
    {
        public async Task<ResultDto> Handle(CalculatePurchaseQueries request, CancellationToken cancellationToken)
        {
            if (CheckJustOne(request.Net.HasValue, request.Gross.HasValue, request.Vat.HasValue))
            {
                Purchase purchase = null;
                if (request.Net.HasValue)
                {
                    purchase = new Net(request.Net.Value);
                }
                else if (request.Gross.HasValue)
                {
                    purchase = new Gross(request.Gross.Value);
                }
                else if (request.Vat.HasValue)
                {
                    purchase = new Gross(request.Vat.Value);
                }
                else
                {
                    return null;
                }
                return purchase.Calculate(request.PurchaseRate);
            }
            throw new Exception("more than one input");
        }
        private bool CheckJustOne(params bool[] items)
        {
            var condition = items.Count(x => x) == 1;
            return condition;
        }
    }
}