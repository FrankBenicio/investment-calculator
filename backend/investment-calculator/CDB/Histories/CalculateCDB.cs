using Cdb.Domain.Dto;
using Cdb.Domain.Interfaces;
using Cdb.Domain.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Cdb.Histories
{
    public class CalculateCDB : ICalculateCDB
    {
        private readonly IValidator<CdbRequest> validator;

        public CalculateCDB(IValidator<CdbRequest> validator)
        {
            this.validator = validator;
        }

        public async Task<CdbResponse> Execute(CdbRequest cdbRequest)
        {
            ValidationResult paramsValidation = await validator.ValidateAsync(cdbRequest);

            if (!paramsValidation.IsValid)
                throw new ValidationException(errors: paramsValidation.Errors);

            var taxPercentage = GetTaxPercentage(cdbRequest.Month);
            var grossTotalAmount = CalculateInvestment(cdbRequest);
            var interestAmount = CalculateInterest(cdbRequest.InvestmentValue, grossTotalAmount, taxPercentage);
            var netValue = CalculateNetWorth(grossTotalAmount, interestAmount);

            var cdbResponse = new CdbResponse
                (grossTotalAmount: grossTotalAmount,
                amountInvested: cdbRequest.InvestmentValue,
                interestAmount: interestAmount,
                netTotalAmount: netValue
                );

            return cdbResponse;
        }

        private double CalculateInvestment(CdbRequest cdbRequest)
        {
            double value = cdbRequest.InvestmentValue;
            int month = cdbRequest.Month;

            var tbValue = 108 / 100;
            var cdiValue = 0.9 / 100;

            for (int i = 0; i < month; i++)
            {
                value = Math.Round(value * (1 + (tbValue * cdiValue)), 2, MidpointRounding.ToZero);

            }

            return value;
        }

        private double GetTaxPercentage(int month)
        {

            if (month <= 6)
                return (22.5 / 100);
            else if (month <= 12)
                return (20.0 / 100);
            else if (month <= 24)
                return (17.6 / 100);
            else
                return (15.0 / 100);

        }

        private double CalculateInterest(double investmentValue, double grossTotalAmount, double taxPercentage)
        {
            return Math.Round((grossTotalAmount - investmentValue) * taxPercentage, 2, MidpointRounding.ToZero);
        }

        private double CalculateNetWorth(double grossTotalAmount, double interestAmount)
        {
            return Math.Round(grossTotalAmount - interestAmount, 2, MidpointRounding.ToZero);
        }
    }
}
