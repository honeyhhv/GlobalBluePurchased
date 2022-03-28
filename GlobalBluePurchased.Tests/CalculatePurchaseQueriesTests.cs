using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using GlobalBluePurchased.Domain.Core.Models;
using GlobalBluePurchased.Domain.Handler;
using GlobalBluePurchased.Domain.Request;
using GlobalBluePurchased.Domain.Resources.ResourceManagers.Interface;
using Moq;
using NUnit.Framework;

namespace GlobalBluePurchased.Tests
{
    public class CalculatePurchaseQueriesTests
    {
        private CalculatePurchaseQueriesHandler _handler;
        private CalculatePurchaseValidator _validator;
        [SetUp]
        public void Setup()
        {
            _validator = new CalculatePurchaseValidator(new MockResourceManager());

            _handler = new CalculatePurchaseQueriesHandler(new MockResourceManager());
        }

        [Test]
        public async Task CalculatePurchaseQueries_FailedValidationTest()
        {
            var query = new CalculatePurchaseQueries();
            var validatorResult = await _validator.ValidateAsync(query, CancellationToken.None);
            validatorResult.IsValid.Should().BeFalse();
        }
        [Test]
        public async Task CalculatePurchaseQueries_ValidationNormalTest()
        {
            var query = new CalculatePurchaseQueries()
            {
                PurchaseRate = PurchaseRate.TenPercent,
                Gross = 1500
            };
            var validatorResult = await _validator.ValidateAsync(query, CancellationToken.None);
            validatorResult.IsValid.Should().BeTrue();
        }
        
        [Test]
        public async Task CalculatePurchaseQueries_ValidationNormalTest()
        {
            var query = new CalculatePurchaseQueries()
            {
                PurchaseRate = PurchaseRate.TenPercent,
                Gross = 1500
            };
            var validatorResult = await _validator.ValidateAsync(query, CancellationToken.None);
            validatorResult.IsValid.Should().BeTrue();
        }
    }
}