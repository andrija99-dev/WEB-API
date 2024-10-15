using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repostitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UploadRestaurantLogo
{
    internal class UploadRestaurantLogoCommandHandler(ILogger<UploadRestaurantLogoCommandHandler> logger, 
        IRestaurantsRepository restaurantsRepository, IRestaurantAuthorizationService restaurantAuthorizationService, IBlobStorageService blobStorageService) : IRequestHandler<UploadRestaurantLogoCommand>
    {

        public async Task Handle(UploadRestaurantLogoCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Uploading restaurant logo for id: {RestaurantId}", request.RestaurantId);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException("Restaurant", request.RestaurantId.ToString());
            }
            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
            {
                throw new ForbidException();
            }

            var logoUrl = await blobStorageService.UploadToBlobAsync(request.File, request.FileName);

            restaurant.LogoUrl = logoUrl;
            await restaurantsRepository.SaveChanges();
        }







    }
}

