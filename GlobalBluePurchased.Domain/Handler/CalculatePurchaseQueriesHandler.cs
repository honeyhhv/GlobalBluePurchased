using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GlobalBluePurchased.Domain.Core.Models;
using GlobalBluePurchased.Domain.Core.Models.ValueObjects;
using GlobalBluePurchased.Domain.Request;
using GlobalBluePurchased.Domain.Resources;
using GlobalBluePurchased.Domain.Resources.ResourceManagers.Interface;
using MediatR;

namespace GlobalBluePurchased.Domain.Handler
{
    public class CalculatePurchaseQueriesHandler : IRequestHandler<CalculatePurchaseQueries, ResultDto>
    {
        private IResourceManager resourceManager;

        public CalculatePurchaseQueriesHandler(IResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        public async Task<ResultDto> Handle(CalculatePurchaseQueries request, CancellationToken cancellationToken)
        {
            if (CheckJustOne(request.Net.HasValue, request.Gross.HasValue, request.Vat.HasValue))
            {
                Purchase purchase;
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
            throw new Exception(resourceManager[SharedResource.InputErrorMessage]);
        }
        private bool CheckJustOne(params bool[] items)
        {
            var condition = items.Count(x => x) == 1;
            return condition;
        }
    }
}