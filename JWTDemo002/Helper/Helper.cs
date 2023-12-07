using JWTDemo002.Enums;
using JWTDemo002.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTDemo002.Helper
{
    public class Helper
    {
        public static string JWTToken(User user, string jwtSecretKey)
        {
            try
            {
                List<Claim> claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Username),
                new (ClaimTypes.Role, Role.user.ToString())
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);


                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: cred
                    );
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
