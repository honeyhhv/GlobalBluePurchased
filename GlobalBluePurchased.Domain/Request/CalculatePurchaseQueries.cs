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
            
            RuleFor(x => x).Must(item => CheckJustOne(item.Net.HasValue, item.Gross.HasValue, item.Vat.HasValue)).WithName(resourceManager[SharedResource.FiledsNameForInput]).WithMessage(resourceManager[SharedResource.InputErrorMessage]);

            RuleFor(x => x.Net).GreaterThan(0).When(x => x.Net.HasValue).WithMessage(resourceManager[SharedResource.GreatharThanZero]);
            RuleFor(x => x.Gross).GreaterThan(0).When(x => x.Gross.HasValue).WithMessage(resourceManager[SharedResource.GreatharThanZero]);
            RuleFor(x => x.Vat).GreaterThan(0).When(x => x.Vat.HasValue).WithMessage(resourceManager[SharedResource.GreatharThanZero]);


        }

        private bool CheckJustOne(params bool[] items)
        {
            var condition = items.Count(x => x) == 1;
            return condition;
        }
    }



}