using Rayify.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {
        
    }
    public class LoginUserCommandSuccessResponse: LoginUserCommandResponse 
    {
        public Token Token { get; set; }
    }
    public class LoginUserCommandFailedResponse: LoginUserCommandResponse
    {
        public string Message { get; set; }
        public string Code { get; set; }
    }

}
