using System.Linq;
using FluentValidation;
using GlobalBluePurchased.Domain.Core.Models;
using GlobalBluePurchased.Domain.Core.Models.ValueObjects;
using GlobalBluePurchased.Domain.Resources;
using GlobalBluePurchased.Domain.Resources.ResourceManagers.Interface;
using MediatR;

namespace GlobalBluePurchased.Domain.Request
{
    public class CalculatePurchaseQueries : IRequest<ResultDto>
    {
        public decimal? Net { get; set; }
        public decimal? Gross { get; set; }
        public decimal? Vat { get; set; }
        public PurchaseRate PurchaseRate { get; set; }
    }

    public class CalculatePurchaseValidator : AbstractValidator<CalculatePurchaseQueries>
    {
        public CalculatePurchaseValidator(IResourceManager resourceManager)
        {
            RuleFor(x => x.PurchaseRate).IsInEnum().WithMessage(resourceManager[SharedResource.OutOfRangeErrorMessage]);
        }
    }



}