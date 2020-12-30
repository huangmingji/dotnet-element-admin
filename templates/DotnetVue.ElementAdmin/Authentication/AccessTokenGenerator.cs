using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;

namespace DotnetVue.ElementAdmin.Authentication
{
    public class AccessTokenGenerator
    {
        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="securityKey">token加密key</param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <param name="userId"></param>
        /// <param name="expires"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public static string GenerateToken(string securityKey, string issuer, string audience, string userId, DateTime expires, List<string> permissions)
        {
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