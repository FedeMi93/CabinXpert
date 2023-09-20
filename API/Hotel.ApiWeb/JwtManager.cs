using Hotel.WebApi.DTOs;
using Microsoft.IdentityModel.Tokens;
using Obligatorio_1.Entidades;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Hotel.WebApi
{
    public class JwtManager
    {
        
        
        public static string CreateToken(User user, IConfiguration configuration)
        {
           List<Claim> claims = new List<Claim>() { 
                new Claim(ClaimTypes.Email, user.Email)
           };

            var secretPassword = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes
              (configuration.GetSection("AppSettings:SecretTokenKey").Value!));

            var credentials = new SigningCredentials(secretPassword, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}
