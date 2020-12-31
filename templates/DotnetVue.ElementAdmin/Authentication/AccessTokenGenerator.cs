using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IdentityModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DotnetVue.ElementAdmin.Authentication
{
    public class AccessTokenGenerator : IAccessTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public AccessTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string userId, List<string> permissions, DateTime expires)
        {
            var securityKey = _configuration.GetSection("Jwt:SecurityKey").Value;
            var issuer = _configuration.GetSection("Jwt:Issuer").Value;
            var audience = _configuration.GetSection("Jwt:Audience").Value;

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtClaimTypes.Subject, userId));
            claims.Add(new Claim(JwtClaimTypes.JwtId, userId));
            foreach (string permission in permissions)
            {
                claims.Add(new Claim("permission", permission));
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(securityKey));
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: expires,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}