using Xunit;
using Restaurants.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Domain.Constants;
using FluentAssertions;

namespace Restaurants.Application.Users.Tests
{
    public class CurrentUserTests
    {
        //TestMethod_Scenario_ExcepctedResult
        [Theory()]
        [InlineData(UserRoles.Admin)]
        [InlineData(UserRoles.User)]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
        {
            //arange
            var currentUser = new CurrentUser("1","test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

            //act
            var isInRole = currentUser.IsInRole(roleName);

            //assert
            isInRole.Should().BeTrue();
        }

        //TestMethod_Scenario_ExcepctedResult
        [Fact()]
        public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
        {
            //arange
            var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

            //act
            var isInRole = currentUser.IsInRole(UserRoles.Owner);

            //assert
            isInRole.Should().BeFalse();
        }

        [Fact()]
        public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
        {
            //arange
            var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

            //act
            var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());

            //assert
            isInRole.Should().BeFalse();
        }
    }
}