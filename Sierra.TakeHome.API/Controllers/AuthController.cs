using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sierra.TakeHome.API.Models;

namespace Sierra.TakeHome.API.Controllers; 

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase {

    /// <summary>
    /// 
    /// </summary>
    private readonly IConfiguration _configuration;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    public AuthController(IConfiguration configuration) {
        _configuration = configuration;
    }
    
    /// <summary>
    /// 
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenModel))]
    public IActionResult Authenticate([FromBody] AuthModel request) {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sid, request.CustomerId.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return Ok(new TokenModel {
            Token = jwtToken
        });
    }
}