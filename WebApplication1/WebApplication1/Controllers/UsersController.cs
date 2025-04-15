using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController(JwtOptions jwtOptions) : ControllerBase
    {
        [HttpPost]
        [Route("auth")]

        public ActionResult<string> AuthenticateUser(AuthenticationRequest request, ApplicationDbContext dbContext)
        {
            var user = dbContext.Set<User>().FirstOrDefault(x => x.Name == request.UserName && x.Password == request.Password);
            if (user == null) return Unauthorized();
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtOptions.Issuer,
                Audience = jwtOptions.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SingingKey)),
                SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity (new Claim[]
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Name, user.Name)
                })
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);
            return Ok(accessToken);
        }
    }
}
 