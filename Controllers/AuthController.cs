using Microsoft.AspNetCore.Http;
using BankingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace BankingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<BankUser> _userManager;
        private readonly SignInManager<BankUser> _signInManager;
        private readonly IConfiguration _config;
        public AuthController(UserManager<BankUser> userManager, SignInManager<BankUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = new BankUser { UserName = request.Email, Email = request.Email, Name = request.Name };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return Ok("User registered successfully");
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return Unauthorized("Invalid credentials");
            }
            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }
        private string GenerateJwtToken(BankUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
