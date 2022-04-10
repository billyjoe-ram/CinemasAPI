using UsuariosAPI.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace UsuariosAPI.Services
{
    public class TokenService
    {
        public Token CreateToken(IdentityUser<int> usuario)
        {
            Claim[] claimsUsuario = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn")
            );
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                claims: claimsUsuario,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddHours(3)
            );
            var jwtTokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new Token(jwtTokenString);
        }
    }
}
