
using Castle.Core.Resource;
using Cdb.Domain.Dto;
using Cdb.Histories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CdbTests.HistoriesTests
{
    public class CalculateCDBTests
    {
        [Fact]
        public async Task ShouldCalculateTheInvestmentInCDBUpToSixMonths()
        {
            var cdbRequest = new CdbRequest
            {
                InvestmentValue = 1000,
                Month = 6
            };

            var validator = new InlineValidator<CdbRequest>();
            var calculateCDB = new CalculateCDB(validator);

            var result = await calculateCDB.Execute(cdbRequest);

            Assert.NotNull(result);
            Assert.Equal(1042.78, result.NetTotalAmount);
            Assert.Equal(12.42, result.InterestAmount);
            Assert.Equal(1000, result.AmountInvested);
            Assert.Equal(1055.2, result.GrossTotalAmount);
        }

        [Fact]
        public async Task ShouldCalculateTheInvestmentInCDBUpToTwelveMonths()
        {
            var cdbRequest = new CdbRequest
            {
                InvestmentValue = 1000,
                Month = 12
            };

            var validator = new InlineValidator<CdbRequest>();
            var calculateCDB = new CalculateCDB(validator);

            var result = await calculateCDB.Execute(cdbRequest);

            Assert.NotNull(result);
            Assert.Equal(1090.76, result.NetTotalAmount);
            Assert.Equal(22.69, result.InterestAmount);
            Assert.Equal(1000, result.AmountInvested);
            Assert.Equal(1113.45, result.GrossTotalAmount);
        }

        [Fact]
        public async Task ShouldCalculateTheInvestmentInCDBUpToTwentyFourMonths()
        {
            var cdbRequest = new CdbRequest
            {
                InvestmentValue = 1000,
                Month = 24
            };

            var validator = new InlineValidator<CdbRequest>();
            var calculateCDB = new CalculateCDB(validator);

            var result = await calculateCDB.Execute(cdbRequest);

            Assert.NotNull(result);
            Assert.Equal(1197.58, result.NetTotalAmount);
            Assert.Equal(42.2, result.InterestAmount);
            Assert.Equal(1000, result.AmountInvested);
            Assert.Equal(1239.79, result.GrossTotalAmount);
        }

        [Fact]
        public async Task ShouldCalculateTheInvestmentInCDBUpToTwentyFiveMonths()
        {
            var cdbRequest = new CdbRequest
            {
                InvestmentValue = 1000,
                Month = 25
            };

            var validator = new InlineValidator<CdbRequest>();
            var calculateCDB = new CalculateCDB(validator);

            var result = await calculateCDB.Execute(cdbRequest);

            Assert.NotNull(result);
            Assert.Equal(1213.3, result.NetTotalAmount);
            Assert.Equal(37.64, result.InterestAmount);
            Assert.Equal(1000, result.AmountInvested);
            Assert.Equal(1250.94, result.GrossTotalAmount);
        }

        [Fact]
        public async Task ShouldNotCalculateTheInvestmentWithZeroValues()
        {
            var cdbRequest = new CdbRequest
            {
                InvestmentValue = 0,
                Month = 0
            };

            var validator = new InlineValidator<CdbRequest>();
            validator.RuleFor(x => x.InvestmentValue)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Por favor, informe o valor de investimento.");

            validator.RuleFor(x => x.Month)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Por favor, informe o prazo que seu dinheiro ficará investido.");

            var calculateCDB = new CalculateCDB(validator);

            Func<Task> act = () => calculateCDB.Execute(cdbRequest);

            var validationException = await Assert.ThrowsAsync<ValidationException>(act);

            var errors1 = validationException.Errors.ToList()[0];
            var errors2 = validationException.Errors.ToList()[1];

            Assert.Equal("Por favor, informe o valor de investimento.", errors1.ErrorMessage);
            Assert.Equal("Por favor, informe o prazo que seu dinheiro ficará investido.", errors2.ErrorMessage);

        }

        [Fact]
        public async Task ShouldNotCalculateTheInvestmentWithZeroInvestmentValue()
        {
            var cdbRequest = new CdbRequest
            {
                InvestmentValue = 0,
                Month = 1
            };

            var validator = new InlineValidator<CdbRequest>();
            validator.RuleFor(x => x.InvestmentValue)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Por favor, informe o valor de investimento.");

            validator.RuleFor(x => x.Month)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Por favor, informe o prazo que seu dinheiro ficará investido.");

            var calculateCDB = new CalculateCDB(validator);

            Func<Task> act = () => calculateCDB.Execute(cdbRequest);

            var validationException = await Assert.ThrowsAsync<ValidationException>(act);

            var errors1 = validationException.Errors.ToList()[0];

            Assert.Equal("Por favor, informe o valor de investimento.", errors1.ErrorMessage);
        }

        [Fact]
        public async Task ShouldNotCalculateTheInvestmentWithZeroMonthValue()
        {
            var cdbRequest = new CdbRequest
            {
                InvestmentValue = 1000,
                Month = 0
            };

            var validator = new InlineValidator<CdbRequest>();
            validator.RuleFor(x => x.InvestmentValue)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Por favor, informe o valor de investimento.");

            validator.RuleFor(x => x.Month)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Por favor, informe o prazo que seu dinheiro ficará investido.");

            var calculateCDB = new CalculateCDB(validator);

            Func<Task> act = () => calculateCDB.Execute(cdbRequest);

            var validationException = await Assert.ThrowsAsync<ValidationException>(act);

            var errors1 = validationException.Errors.ToList()[0];

            Assert.Equal("Por favor, informe o prazo que seu dinheiro ficará investido.", errors1.ErrorMessage);

        }
    }
}
