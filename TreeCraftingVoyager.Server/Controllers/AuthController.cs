using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using TreeCraftingVoyager.Server.Attributes.Filter;
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
    private readonly IUserStore<Account> _userStore;
    private readonly IUserEmailStore<Account> _emailStore;
    private readonly IEmailSender _emailSender;

    public AuthController(
        ILogger<AuthController> logger,
        UserManager<Account> userManager,
        SignInManager<Account> signInManager,
        IConfiguration configuration,
        IUserStore<Account> userStore,
        IEmailSender emailSender)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _userStore = userStore;
        _emailStore = GetEmailStore();
        _emailSender = emailSender;
    }

    [HttpPost("register")]
    [RateLimit(5, 60)]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (model == null)
        {
            return BadRequest("Invalid client request");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new Account
        {
            UserName = model.Email,
            Email = model.Email
        };

        await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            _logger.LogInformation("User created a new account with password.");

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "Auth",
                new { userId = userId, code = code },
            protocol: HttpContext.Request.Scheme);

            var emailContent = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
            await _emailSender.SendEmailAsync(model.Email, "Confirm your email", emailContent);

            if (_userManager.Options.SignIn.RequireConfirmedAccount)
            {
                return Ok(new { Result = "User created successfully. Confirmation email sent." });
                //return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
            }
            else
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok(new { Result = "User created and signed in successfully" });
                //return LocalRedirect(returnUrl);
            }
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("message", error.Description);
        }

        return BadRequest(ModelState);
    }

    [HttpPost("login")]
    [RateLimit(10, 60)]
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

                //return LocalRedirect(returnUrl);
            }

            if (result.RequiresTwoFactor)
            {
                // return RedirectToPage("./LoginWith2fa", new { ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                
                // return RedirectToPage("./Lockout");

                ModelState.AddModelError("message", "This account has been locked out, please try again later.");
                return BadRequest(ModelState);
            }
        }

        ModelState.AddModelError("message", "Invalid login attempt.");
        return BadRequest(ModelState);
    }

    [HttpGet("check")]
    [RateLimit(100, 60)]
    public async Task<IActionResult> Check()
    {
        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                return Ok(new
                {
                    isAuthenticated = true,
                    id = user.Id,
                    email = user.Email,
                    
                    // claims = userClaims,
                    // roles = userRoles
                });
            }
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

    [HttpGet("confirm-email")]
    [RateLimit(5, 60)]
    public async Task<IActionResult> ConfirmEmail(string userId, string code)
    {
        if (userId == null || code == null)
        {
            return BadRequest("User ID and code must be provided.");
            //return RedirectToPage("/Index");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userId}'.");
        }

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);

        if (result.Succeeded)
        {
            return Ok("Thank you for confirming your email.");
        }
        else
        {
            return BadRequest("Error confirming your email.");
        }
    }


    private IUserEmailStore<Account> GetEmailStore()
    {
        if (!_userManager.SupportsUserEmail)
        {
            throw new NotSupportedException("The default UI requires a user store with email support.");
        }
        return (IUserEmailStore<Account>) _userStore;
    }
}
