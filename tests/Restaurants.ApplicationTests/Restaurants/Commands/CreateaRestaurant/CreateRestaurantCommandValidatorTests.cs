using Xunit;
using Restaurants.Application.Restaurants.Commands.CreateaRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace Restaurants.Application.Restaurants.Commands.CreateaRestaurant.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {


        [Fact()]
        //TestMethod_Scenario_ExcepctedResult
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            //arrange
            var command = new CreateRestaurantCommand()
            {
                Name = "Test",
                Category = "Italian",
                ContactEmail = "test@test.com",
                PostalCode = "12-234",
            };

            var validator = new CreateRestaurantCommandValidator();

            //act
            var result = validator.TestValidate(command);  //fluentValidation.Helper


            //assert
            result.ShouldNotHaveAnyValidationErrors();

        }
        [Fact()]

        public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
        {
            //arrange
            var command = new CreateRestaurantCommand()
            {
                Name = "Te",
                Category = "ItA",
                ContactEmail = "@test.com",
                PostalCode = "12234",
            };

            var validator = new CreateRestaurantCommandValidator();

            //act
            var result = validator.TestValidate(command);  //fluentValidation.Helper


            //assert 
            result.ShouldHaveValidationErrorFor(c => c.Name);
            result.ShouldHaveValidationErrorFor(c => c.Category);
            result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);

        }

        [Theory()]
        [InlineData("Italian")]
        [InlineData("meXicAn")]
        [InlineData("jaPaNese")]
        [InlineData("ameRICan")]
        [InlineData("Indian")]
        public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
        {
            //arrange
            var command = new CreateRestaurantCommand()
            {
                Category = category
            };

            var validator = new CreateRestaurantCommandValidator();

            //act
            var result = validator.TestValidate(command);  //fluentValidation.Helper


            //assert
            result.ShouldNotHaveValidationErrorFor(c => c.Category);

        }

        [Theory()]
        [InlineData("10220")]
        [InlineData("102-20")]
        [InlineData("10 220")]
        [InlineData("10-2 20")]
        public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
        {
            var command = new CreateRestaurantCommand()
            {
                PostalCode = postalCode
            };

            var validator = new CreateRestaurantCommandValidator();

            //act
            var result = validator.TestValidate(command);  //fluentValidation.Helper


            //assert
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);
        }


    }
}