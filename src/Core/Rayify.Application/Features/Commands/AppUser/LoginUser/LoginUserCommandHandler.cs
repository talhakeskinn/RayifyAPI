using MediatR;
using Microsoft.AspNetCore.Identity;
using Rayify.Application.Abstractions.Token;
using Rayify.Application.DTOs;
using Rayify.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserEntity = Rayify.Domain.Entities.Identity.AppUser;

namespace Rayify.Application.Features.Commands.AppUser.LoginUser
{

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _singInManager;
        private readonly ITokenHandler _tokenHandler;
        public LoginUserCommandHandler(UserManager<UserEntity> userManager, SignInManager<UserEntity> singInManager, ITokenHandler tokenHandler )
        {
            _userManager = userManager;
            _singInManager = singInManager;
            _tokenHandler = tokenHandler;
        }
        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            UserEntity user = await _userManager.FindByNameAsync(request.username);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            SignInResult result = await _singInManager.CheckPasswordSignInAsync(user, request.password, false);
            if(result.Succeeded) 
            {
                Token token = _tokenHandler.CreateAccessToken(1441, user.UserName);
                return new LoginUserCommandSuccessResponse()
                {
                    Token = token,
                };
            }
            return new LoginUserCommandFailedResponse()
            {
                Message = "Giriş Sırasında hata oluştu !!!",
            };

        }
    }
}
