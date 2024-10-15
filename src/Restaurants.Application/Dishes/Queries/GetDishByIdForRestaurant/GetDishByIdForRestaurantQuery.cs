using MediatR;
using Restaurants.Application.Dishes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant
{
    public class GetDishByIdForRestaurantQuery(int restaurantId, int id) : IRequest<DishDto>
    {
        public int Id { get; set; } = id;
        public int RestaurantId { get; set; } = restaurantId;
    }
}
