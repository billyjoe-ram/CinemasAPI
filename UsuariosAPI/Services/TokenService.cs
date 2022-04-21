using UsuariosAPI.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace UsuariosAPI.Services
{
    /// <summary>
    ///     Service para as operações e regras de negócio relacionadas ao token.
    /// </summary>
    internal class TokenService
    {
        /// <summary>
        ///     Cria um token para o Identity User informado.
        /// </summary>
        /// <param name="usuario">Usuario Identity.</param>
        /// <param name="role">Roles do usuário identity.</param>
        /// <returns>Token gerado pelo service.</returns>
        public Token CreateUserToken(IdentityUser<int> usuario, string role)
        {
            Claim[] claimsUsuario = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, role),
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
