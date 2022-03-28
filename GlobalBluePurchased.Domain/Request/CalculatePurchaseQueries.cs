using System.Linq;
using FluentValidation;
using GlobalBluePurchased.Domain.Core.Models;
using GlobalBluePurchased.Domain.Core.Models.ValueObjects;
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

    public abstract class CalculatePurchaseValidator : AbstractValidator<CalculatePurchaseQueries>
    {
        protected CalculatePurchaseValidator()
        {
            RuleFor(x => x.PurchaseRate).IsInEnum().WithMessage("The Rate is out of range");
        }
    }



}