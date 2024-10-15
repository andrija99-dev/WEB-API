using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users.Commands.UpdateUserDetails
{
    public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger, IUserContext userContext, IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
    {
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Updating user: {UserId}, with {@Request}", user!.Id, request);

            var dbuser = await userStore.FindByIdAsync(user!.Id, cancellationToken);

            if (dbuser == null)
            {
                throw new NotFoundException(nameof(User), user!.Id);
            }

            dbuser.Nationality = request.Nationality;
            dbuser.DateOfBirth = request.DateOfBirth;

            await userStore.UpdateAsync(dbuser, cancellationToken);
        }
    }
}
