using Xunit;
using Restaurants.Application.Restaurants.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Restaurants.Domain.Entities;
using FluentAssertions;
using Restaurants.Application.Restaurants.Commands.CreateaRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

namespace Restaurants.Application.Restaurants.Dtos.Tests
{
    public class RestaurantsProfileTests
    {
            IMapper mapper;
        public RestaurantsProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>    
            {
                cfg.AddProfile<RestaurantsProfile>();
            });

            mapper = configuration.CreateMapper();
        }
        [Fact()]
        //TestMethod_Scenario_ExcepctedResult

        public void CreateMap_ForrestaurantToRestaurantDto_MapsCorrectly()
        {
            //arrange
            

            var restaurant = new Restaurant()
            {
                Id = 1,
                Name = "Test restaurant",
                Description = "Test restaurant",
                Category = "Test restaurant",
                HasDelivery = true,
                ContactEmail = "Test restaurant",
                ContactNumber = "Test restaurant",
                Address = new Address()
                {
                    City = "Test City",
                    Street = "Test street",
                    PostalCode = "12345"
                }
            };

            //act
            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

            //assert
            restaurantDto.Should().NotBeNull();
            restaurantDto.Id.Should().Be(restaurant.Id);
            restaurantDto.Name.Should().Be(restaurant.Name);
            restaurantDto.Description.Should().Be(restaurant.Description);
            restaurantDto.Category.Should().Be(restaurant.Category);
            restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
            restaurantDto.City.Should().Be(restaurant.Address.City);
            restaurantDto.Street.Should().Be(restaurant.Address.Street);
            restaurantDto.PostalCode.Should().Be(restaurant.Address.PostalCode);
        }

        [Fact()]
        public void Createmap_ForCreateRestaurantCommandToRestaurant_Mapsorrectly()
        {
            //arrange
            

            var command = new CreateRestaurantCommand()
            {
                Name = "Test restaurant",
                Description = "Test restaurant",
                Category = "Test restaurant",
                HasDelivery = true,
                ContactEmail = "Test restaurant",
                ContactNumber = "Test restaurant",
                City = "Test City",
                Street = "Test street",
                PostalCode = "12345"
            };

            //act
            var restaurant = mapper.Map<Restaurant>(command);

            //assert
            restaurant.Should().NotBeNull();
            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);
            restaurant.Category.Should().Be(command.Category);
            restaurant.HasDelivery.Should().Be(command.HasDelivery);
            restaurant.ContactEmail.Should().Be(command.ContactEmail);
            restaurant.ContactNumber.Should().Be(command.ContactNumber);
            restaurant.Address.Should().NotBeNull();
            restaurant.Address.City.Should().Be(command.City);
            restaurant.Address.Street.Should().Be(command.Street);
            restaurant.Address.PostalCode.Should().Be(command.PostalCode);
        }

        [Fact()]
        public void Createmap_ForUpdateRestaurantCommandToRestaurant_Mapsorrectly()
        {
            //arrange


            var command = new UpdateRestaurantCommand()
            {
                Id = 1,
                Name = "Test restaurant",
                Description = "Test restaurant",
                HasDelivery = true
            };

            //act
            var restaurant = mapper.Map<Restaurant>(command);

            //assert
            restaurant.Should().NotBeNull();
            restaurant.Id.Should().Be(command.Id);
            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);
            restaurant.HasDelivery.Should().Be(command.HasDelivery);
        }
    }
}