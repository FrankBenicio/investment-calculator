
using Cdb.Domain.Dto;
using Cdb.Domain.Validators;
using FluentValidation.TestHelper;

namespace CdbTests.Domain.ValidatorsTests
{
    public class CdbRequestValidatorTests
    {
        [Fact]
        public void ShouldHaveErrorWhenInvestmentValueIsZero()
        {
            var model = new CdbRequest 
            { 
                InvestmentValue = 0, 
                Month = 1 
            };

            var validator = new CdbRequestValidator();

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.InvestmentValue);
        }

        [Fact]
        public void ShouldHaveErrorWhenMonthIsZero()
        {
            var model = new CdbRequest 
            { 
                InvestmentValue = 1000, 
                Month = 0 
            };

            var validator = new CdbRequestValidator();

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Month);
        }

        [Fact]
        public void ShouldHaveErrorWhenParamsIsZero()
        {
            var model = new CdbRequest 
            { 
                InvestmentValue = 0, 
                Month = 0 
            };

            var validator = new CdbRequestValidator();

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.InvestmentValue);
            result.ShouldHaveValidationErrorFor(x => x.Month);
        }
    }
}
