﻿using Xunit;
using Restaurants.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization.Policy;
using Restaurants.APITests;
using Restaurants.Domain.Repostitories;
using Moq;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Restaurants.Domain.Entities;
using System.Net.Http.Json;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.API.Controllers.Tests
{
    public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock = new();

        public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                    services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantsRepository), _ => _restaurantsRepositoryMock.Object));
                });
            });
        }


        [Fact()]
        public async Task GetAll_ForValidRequest_Returns200Ok()
        {

            //arrange
            var client = _factory.CreateClient();

            //act
            var result = await client.GetAsync("/api/restaurants?pageNumber=1&pageSize=10");

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        }

        [Fact()]
        public async Task GetAll_ForInvalidRequest_Returns400BadRequest()
        {

            //arrange
            var client = _factory.CreateClient();

            //act
            var result = await client.GetAsync("/api/restaurants");

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

        }

        [Fact()]
        public async Task GetById_ForNonExistingId_ShouldReturn404NotFound()
        {

            //arrange
            var id = 1223;

            _restaurantsRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync((Restaurant?)null);
            var client = _factory.CreateClient();

            //act
            var result = await client.GetAsync($"/api/restaurants/{id}");

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

        }

        [Fact()]
        public async Task GetById_ForExistingId_ShouldReturn200Ok()
        {

            //arrange
            var id = 99;

            var restaurant = new Restaurant()
            {
                Id = id,
                Name = "Test",
                Description = "Test Description"
            };

            _restaurantsRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync(restaurant);
            var client = _factory.CreateClient();

            //act
            var response = await client.GetAsync($"/api/restaurants/{id}");
            var restaurantDto = await response.Content.ReadFromJsonAsync<RestaurantDto>();

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            restaurantDto.Should().NotBeNull();
            restaurantDto.Name.Should().Be("Test");
            restaurantDto.Description.Should().Be("Test Description");

        }
    }
}