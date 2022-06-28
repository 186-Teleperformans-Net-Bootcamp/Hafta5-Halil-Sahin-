using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Dto;
using WebAPI.Model;
using WebAPI.Services.RabbitMQ;

namespace WebAPI.Controllers
{
    public class AccountController : Controller
    {
        #region FIELDS

        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenModel tokenOption;
        private readonly IRabbitMqService _mQService;

        #endregion


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            List<Claim> claims = new List<Claim>();
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null) throw new Exception("");

            var result = await _userManager.CheckPasswordAsync(user, login.Password);
            if (result)
            {

                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                claims.Add(new Claim(ClaimTypes.Name, user.Email));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                var token = GetToken(claims);

                var handler = new JwtSecurityTokenHandler();
                string jwt = handler.WriteToken(token);

                return Ok(new
                {
                    token = jwt,
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }


        private JwtSecurityToken GetToken(List<Claim> claims)
        {

            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOption.Key));

            var token = new JwtSecurityToken(

                 signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256),
                 issuer: tokenOption.Issuer,
                 audience: tokenOption.Audience,
                 expires: DateTime.Now.AddMinutes(tokenOption.Expiration),
                 claims: claims
                );

            return token;

        }
    }
}
