using Cdb.Domain.Dto;
using Cdb.Domain.Interfaces;
using FluentValidation;
using investment_calculator.Controllers;
using InvestmentCalculatorTests.CdbApi.Factories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace InvestmentCalculatorTests.CdbApi
{
    public class CdbControllerTests
    {
        [Fact]
        public async Task ShouldCalculateTheInvestmentInCDBUpToOneMonth()
        {
            var cdbRequest = new CdbRequest
            {
                InvestmentValue = 1000,
                Month = 1
            };

            var mockICalculateCDB = new Mock<ICalculateCDB>();
            mockICalculateCDB.Setup(x => x.Execute(cdbRequest)).ReturnsAsync(CdbResponseFactory.Create());
            var api = new CdbController();

            var cdbResult = (await api.Calculate(mockICalculateCDB.Object, cdbRequest)) as ObjectResult;

            Assert.NotNull(cdbResult);

            Assert.Equal(200, cdbResult?.StatusCode);
        }

        [Fact]
        public async Task ShouldNotCalculateTheInvestmentInCDBWithParamsAreZero()
        {
            var cdbRequest = new CdbRequest
            {
                InvestmentValue = 0,
                Month = 0
            };

            var mockICalculateCDB = new Mock<ICalculateCDB>();

            mockICalculateCDB.Setup(x => x.Execute(cdbRequest)).ThrowsAsync(new ValidationException("erros"));

            var api = new CdbController();

            var cdbResult = (await api.Calculate(mockICalculateCDB.Object, cdbRequest)) as ObjectResult;

            Assert.Equal(400, cdbResult?.StatusCode);
        }
    }
}
