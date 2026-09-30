using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RideLinkerAPI.Models;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.DomainService.Interfaces;

namespace RideLinkerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, ILogger<AccountController> logger, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            _logger.LogInformation($"Login attempt for email: {model.Email}");

            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(
                        model.Email,
                        model.Password,
                        false,
                        false
                    );

                    if (result.Succeeded)
                    {
                        var user = await _userManager.FindByEmailAsync(model.Email);

                        _logger.LogInformation($"Login successful for email: {model.Email}");

                        if (user != null)
                        {
                            var token = GenerateJwtTokenAsync(user);
                            return Ok(new { Token = token });
                        }
                        else
                        {
                            _logger.LogError("User is null after successful login.");
                            return StatusCode(500, "Invalid login attempt, wrong email/password combination.");
                        }
                    }
                    else
                    {
                        _logger.LogWarning($"Invalid login attempt.");
                        return StatusCode(500, "Invalid login attempt.");
                    }
                }

                _logger.LogWarning($"Invalid model state for login attempt for email: {model.Email}");
                return BadRequest("Invalid model state.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred during login: {ex.Message}");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            _logger.LogInformation($"Register attempt for email: {model.Email}");

            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "USER");

                    await _signInManager.SignInAsync(user, false);
                    return Ok("Account registered.");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }

            return BadRequest("Invalid registration attempt.");
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logout successful");
        }

        private async Task<string> GenerateJwtTokenAsync(IdentityUser user)
        {
            int userId = await GetCustomUserIdAsync(user.Email);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("Id", userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpirationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<int> GetCustomUserIdAsync(string email)
        {
            User user = await _userService.GetByEmailAsync(email);
            if (user != null)
            {
                return user.Id;
            } else
            {
                return -1;
            }
        }
    }
}
