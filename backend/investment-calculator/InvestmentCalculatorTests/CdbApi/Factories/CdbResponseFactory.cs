using Cdb.Domain.Models;
namespace InvestmentCalculatorTests.CdbApi.Factories
{
    public static class CdbResponseFactory
    {
        public static CdbResponse Create()
        {
            return new CdbResponse
                (
                netTotalAmount: 1006.97,
                grossTotalAmount: 1008.99,
                interestAmount: 2.02,
                amountInvested: 1000
                );
        }
    }
}
