﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rayify.Application.Abstractions.Token;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Infrastructure.Services.Token
{

    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _conf;
        public TokenHandler(IConfiguration configuration)
        {
            _conf = configuration;
        }
        public Application.DTOs.Token CreateAccessToken(int minute, string username)
        {
           Application.DTOs.Token token = new();
           SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_conf["JWTOptions:SigningKey"]));
           SigningCredentials signingCredentials = new(securityKey,SecurityAlgorithms.HmacSha256);
           token.Expiration = DateTime.UtcNow.AddMinutes(minute);
            List<Claim> claims = new List<Claim>
            {
                new Claim("username", username),
            };
            JwtSecurityToken securityToken = new(
                audience: _conf["JWTOptions:Audience"],
                issuer: _conf["JWTOptions:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials:signingCredentials,
                claims: claims
                
                );

            JwtSecurityTokenHandler tokenHandler = new();
            
            
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            return token;

        }
    }
}
