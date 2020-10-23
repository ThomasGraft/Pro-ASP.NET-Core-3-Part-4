using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Blazor.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace Advanced.Controllers
{
    [ApiController]
    [Route("/api/account")]
    public class ApiAccountController : ControllerBase
    {
        private SignInManager<IdentityUser> signInManager;
        private UserManager<IdentityUser> userManager;
        private IConfiguration configuration;

        public ApiAccountController(SignInManager<IdentityUser> mgr, UserManager<IdentityUser> userMgr, IConfiguration config)
        {
            signInManager = mgr;
            userManager = userMgr;
            configuration = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]Credentials creds)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = 
                await signInManager.PasswordSignInAsync(creds.UserName, creds.Password, false, false);

            if (result.Succeeded)
            {
                return Ok();
            }
            return Unauthorized();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody]Credentials creds)
        {
            if (await CheckPassword(creds))
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                byte[] secret = Encoding.ASCII.GetBytes(configuration["jwtsecret"]);
                SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, creds.UserName)
                    }),
                    Expires = DateTime.UtcNow.AddHours(24),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                SecurityToken token = handler.CreateToken(descriptor);
                return Ok(new
                {
                    success = true,
                    token = handler.WriteToken(token)
                });
            }
            return Unauthorized();
        }

        private async Task<bool> CheckPassword(Credentials creds)
        {
            IdentityUser user = await userManager.FindByNameAsync(creds.UserName);
            if (user != null)
            {
                foreach (IPasswordValidator<IdentityUser> v in userManager.PasswordValidators)
                {
                    if ((await v.ValidateAsync(userManager, user, creds.Password)).Succeeded)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public class Credentials
        {
            [Required]
            public string UserName { get; set; }

            [Required]
            public string Password { get; set; }
        }
    }
}
