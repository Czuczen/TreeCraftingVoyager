using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TreeCraftingVoyager.Server.Models.Management;

namespace TreeCraftingVoyager.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly UserManager<Account> _userManager;
    private readonly SignInManager<Account> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(
        ILogger<AuthController> logger,
        UserManager<Account> userManager,
        SignInManager<Account> signInManager,
        IConfiguration configuration)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (model == null)
        {
            return BadRequest("Invalid client request");
        }

        var user = new Account { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            //await _userManager.AddClaimAsync(user, new Claim("permission", "view_logs")); exsample
            return Ok(new { Result = "User created successfully" });
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (model == null)
        {
            return BadRequest("Invalid client request");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var token = await GenerateJwtToken(user, model.RememberMe);
                return Ok(new
                {
                    token = token,
                    id = user.Id,
                    email = user.Email
                });
            }

            if (result.RequiresTwoFactor)
            {
                // Uncomment and implement 2FA if required
                // return RedirectToPage("./LoginWith2fa", new { ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                // Uncomment and implement lockout page if required
                // return RedirectToPage("./Lockout");

                ModelState.AddModelError(string.Empty, "This account has been locked out, please try again later.");
                return BadRequest(ModelState);
            }
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return BadRequest(ModelState);
    }

    [HttpGet("check")]
    public async Task<IActionResult> Check()
    {
        if (User.Identity.IsAuthenticated)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            return Ok(new
            {
                isAuthenticated = true,
                id = userId,
                email = userEmail
            });
        }

        return Ok(new { isAuthenticated = false });
    }

    private async Task<string> GenerateJwtToken(Account user, bool rememberMe)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };


        //var userClaims = await _userManager.GetClaimsAsync(user);
        //claims.AddRange(userClaims);

        //var roles = await _userManager.GetRolesAsync(user);
        //foreach (var role in roles)
        //{
        //    claims.Add(new Claim(ClaimTypes.Role, role));
        //    var roleClaims = await _roleManager.GetClaimsAsync(await _roleManager.FindByNameAsync(role));
        //    claims.AddRange(roleClaims);
        //}

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: rememberMe ? DateTime.UtcNow.AddDays(7) : DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds
        );

        var ret = new JwtSecurityTokenHandler().WriteToken(token);
        _logger.LogDebug($"Generated Token: {ret}");

        return ret;
    }
}
