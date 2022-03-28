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

            _handler = new CalculatePurchaseQueriesHandler();
        }

        [Test]
        public async Task CalculatePurchaseQueries_FailedValidationTest()
        {
            var query = new CalculatePurchaseQueries()
            {
                Gross = 0
            };
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
        public async Task CalculatePurchaseQueries_NetHasValueNormalTest()
        {
            var query = new CalculatePurchaseQueries()
            {
                PurchaseRate = PurchaseRate.TenPercent,
                Net = 1000
            };
            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);

            result.Gross.Value.Should().Be(1100);
            result.Vat.Value.Should().Be(100);
        }
        [Test]
        public async Task CalculatePurchaseQueries_GrossHasValueNormalTest()
        {
            var query = new CalculatePurchaseQueries()
            {
                PurchaseRate = PurchaseRate.ThirteenPercent,
                Gross = 1695
            };
            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);

            result.Net.Value.Should().Be(1500);
            result.Vat.Value.Should().Be(195);
        }
        [Test]
        public async Task CalculatePurchaseQueries_VatHasValueNormalTest()
        {
            var query = new CalculatePurchaseQueries()
            {
                PurchaseRate = PurchaseRate.TwentyPercent,
                Vat = 300
            };
            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);

            result.Net.Value.Should().Be(1500);
            result.Gross.Value.Should().Be(1800);
        }
    }
}