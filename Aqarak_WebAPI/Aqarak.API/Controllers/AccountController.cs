using Aqarak_WebAPI.DTOs;
using Aqarak_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Aqarak_WebAPI.Aqarak.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<AppUser> userManager , IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO UserFromRequest)
        {
            if(ModelState.IsValid)
            {
                var existingUser = await userManager.FindByEmailAsync(UserFromRequest.Email);
                if (existingUser != null)
                    return BadRequest("Email already exists");
                AppUser user = new AppUser();
                user.FullName = UserFromRequest.FullName;
                user.Phone = UserFromRequest.Phone;
                user.Email = UserFromRequest.Email;
                user.UserName = UserFromRequest.Email;
                IdentityResult result = await userManager.CreateAsync(user, UserFromRequest.Password);
                if (result.Succeeded)
                {
                    return Ok("Created");
                }
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("Password", item.Description);
                }
            }
            return BadRequest(ModelState);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LogInDTO userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userFromDb = await userManager.FindByEmailAsync(userDto.Email);
            if (userFromDb == null)
                return Unauthorized("Invalid Email or Password");

            var isCorrect = await userManager.CheckPasswordAsync(userFromDb, userDto.Password);
            if (!isCorrect)
                return Unauthorized("Invalid Email or Password");

            // Claims
            List<Claim> userClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userFromDb.Id),
                new Claim(ClaimTypes.Name, userFromDb.UserName)
            };


            // JWT Config
            var key = configuration["JWT:Key"];
            var issuer = configuration["JWT:Issuer"];
            var audience = configuration["JWT:Audience"];
            var duration = double.Parse(configuration["JWT:DurationInDays"]);

            var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256);

            var expirationDate = DateTime.Now.AddDays(duration);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: expirationDate,
                claims: userClaims,
                signingCredentials: signingCredentials
            );

            AuthResultDTO result = new AuthResultDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expirationDate
            };

            return Ok(result);
        }

    }
}
