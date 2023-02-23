using ApiSouMaisFit.Services;
using ApiSouMaisFit.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace ApiSouMaisFit.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IAuthenticate _authenticate;

    public AccountController(IConfiguration configuration, IAuthenticate authenticate)
    {
        _configuration = configuration;
        _authenticate = authenticate;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<UserToken>> CreateUser([FromBody] RegisterModel model)
    {   
        if(model.Password != model.ConfirmPassword)
        {
            ModelState.AddModelError("ConfirmPassword", "As Senhas não conferem");
            return BadRequest(ModelState);
        }
        
        var user = await _authenticate.RegisterUser(model.Email, model.Password);
        if (user)
            return Ok(user);

        ModelState.AddModelError("CreateUser", "Registro Inválido !");
        return BadRequest("Usuário ou senha inválidos!");
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserToken>> LoginUser([FromBody] LoginModel loginModel)
    {
        var result = await _authenticate.Authenticate(loginModel.Email, loginModel.Password);

        if (result)
            return GenerateToken(loginModel);

        ModelState.AddModelError("LoginUser", "Login Inválido");
        return BadRequest(ModelState);
    }

    private ActionResult<UserToken> GenerateToken(LoginModel loginModel)
    {
        var claims = new[]
        {
            new Claim("email", loginModel.Email),
            new Claim("meuToken", "Token JWT"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddMinutes(20);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: creds
        );

        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration,
        };
    }
}
