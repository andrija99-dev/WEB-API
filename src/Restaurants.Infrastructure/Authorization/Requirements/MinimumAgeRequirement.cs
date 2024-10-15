using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class MinimumAgeRequirement(int minumumAge) : IAuthorizationRequirement
    {
        public int MinimumAge { get; set; } = minumumAge;
    }
}
