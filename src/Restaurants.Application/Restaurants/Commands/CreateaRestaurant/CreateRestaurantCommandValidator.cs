using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.CreateaRestaurant
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        private readonly List<string> validCategories = ["italian", "mexican", "japanese", "american", "indian"];
        public CreateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 100);

            RuleFor(dto => dto.Category)
                .Must(category => validCategories.Contains(category.ToLower())).WithMessage("Invalid Category, Please choose from the valid categories.")
                .When(dto => !string.IsNullOrEmpty(dto.Category));
            
            RuleFor(dto => dto.ContactEmail)
                .EmailAddress().WithMessage("Please provide valid email address");

            RuleFor(dto => dto.PostalCode)
                .Matches(@"^\d{2}-\d{3}$").WithMessage("Please provide valid postal code (XX-XXX).");
        }
    }
}
