using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        public CreateUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(new Domain.Entities.Identity.AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.Username,

            }, request.Password);
            CreateUserCommandResponse response = new() { Succeeded = result.Succeeded };
            if(result.Succeeded)
            {
                response.Message = "Kullanıcı başarıyla kaydedildi !";
            }
            else
            {
                foreach(var err in result.Errors)
                {
                    response.Message += $"{err.Code} - {err.Description}";
                }
            }
            return response;
                }
    }
}
