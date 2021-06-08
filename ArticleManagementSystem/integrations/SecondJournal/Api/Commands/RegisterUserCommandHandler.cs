using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Api.Commands
{
    public class RegisterUserCommandHandler : AsyncRequestHandler<RegisterUserCommand>
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        public readonly AppDbContext _dbContext;

        public RegisterUserCommandHandler(UserManager<IdentityUser<Guid>> userManager, AppDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        protected override async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new IdentityUser<Guid>
            {
                UserName = request.UserName,
                Email = request.Email
            };

            var identityUserResult = await _userManager.CreateAsync(user);

            if (identityUserResult.Succeeded)
                identityUserResult = await _userManager.AddPasswordAsync(user, request.Password);


            if (identityUserResult.Succeeded)
                await _dbContext.SaveChangesAsync();
        }
    }
}