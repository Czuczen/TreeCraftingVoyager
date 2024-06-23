﻿using Microsoft.AspNetCore.Identity;
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

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var token = GenerateJwtToken(user);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

    [HttpGet("check")]
    public async Task<IActionResult> Check()
    {
        //var user = await _userManager.GetUserAsync(User);
        var user = await _userManager.FindByEmailAsync("aa@wp.pl");
        var token = GenerateJwtToken(user);

        var sendedToken = "";
        var authHeader = Request.Headers["Authorization"].FirstOrDefault();
        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            sendedToken = authHeader.Substring("Bearer ".Length).Trim();
        }

        if (token == sendedToken)
        {
            return Ok(new { isAuthenticated = true });
        }

        return Ok(new { isAuthenticated = false });

        // not working 
        // ERROR 22.06.2024 19:28:19,116 || Authentication failed: IDX14100: JWT is not well formed, there are no dots(.).
        // The token needs to be in JWS or JWE Compact Serialization Format. (JWS): 'EncodedHeader.EndcodedPayload.EncodedSignature'. (JWE): 'EncodedProtectedHeader.EncodedEncryptedKey.EncodedInitializationVector.EncodedCiphertext.EncodedAuthenticationTag'.
        //if (User.Identity.IsAuthenticated)
        //{
        //    return Ok(new { isAuthenticated = true });
        //}

        //return Ok(new { isAuthenticated = false });
    }

    private string GenerateJwtToken(Account user)
    {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            var sdf = new JwtSecurityToken();
            var ret = new JwtSecurityTokenHandler().WriteToken(token);
            _logger.LogDebug($"Generated Token: {ret}");

            return ret;
        }
}