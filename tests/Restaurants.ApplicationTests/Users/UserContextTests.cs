using Xunit;
using Restaurants.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Restaurants.Domain.Constants;
using FluentAssertions;

namespace Restaurants.Application.Users.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUserTest_ViewAuthenticatedUser_ShouldReturnCurrentUser()
        {

            //arrange
            var dateOfBirth = new DateOnly(1990, 1, 1);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier,"1"),
                new(ClaimTypes.Email,"test@test.com"),
                new(ClaimTypes.Role,UserRoles.Admin),
                new(ClaimTypes.Role,UserRoles.User),
                new("Nationality","German"),
                new("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));       
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });

            var userContext = new UserContext(httpContextAccessorMock.Object);
            //act

            var currentUser = userContext.GetCurrentUser();

            //assuet
            currentUser.Should().NotBeNull();
            currentUser.Id.Should().Be("1");
            currentUser.Email.Should().Be("test@test.com");
            currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
            currentUser.Nationality.Should().Be("German");
            currentUser.DateOfBirth.Should().Be(dateOfBirth);


        }

        [Fact]
        public void GetCurrentUserTest_ViewAuthenticatedUser_ThrowsInvalidOperationException()
        {
            //arrange
            var httpcontextAccessorMock = new Mock<IHttpContextAccessor>();
            httpcontextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

            var usercontext = new UserContext(httpcontextAccessorMock.Object);
            //act

            Action action = () =>  usercontext.GetCurrentUser();

             //assert
             action.Should().Throw<InvalidOperationException>()
                .WithMessage("User context is not present");



        }

    }
}