using Core.Entities;
using Core.Tokens.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ToDoAssignment.Models.Tokens.Response;
using ToDoAssignment.Models.Users.Entity;
using ToDoAssignment.Service.JWT.Services.Abstracts;

namespace ToDoAssignment.Service.JWT.Services.Concretes;

public class JwtService : IJwtService
{
    private readonly CustomTokenOptions tokenOptions;
    private readonly UserManager<User> _userManager;

    public JwtService(IOptions<CustomTokenOptions> options, UserManager<User> userManager)
    {
        tokenOptions = options.Value;
        _userManager = userManager;
    }

    public async Task<TokenResponseDto> CreateToken(User user)
    {
        var accessTokenExpiration = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration);
        var seurityKey = SecurityKeyHelper.GetSecurityKey(tokenOptions.SecurityKey);

        SigningCredentials signingCredentials = new(seurityKey, SecurityAlgorithms.HmacSha512Signature);

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: tokenOptions.Issuer,
            expires: accessTokenExpiration,
            claims: await GetClaims(user,tokenOptions.Audience),
            signingCredentials: signingCredentials
        );

        var jwtHandler = new JwtSecurityTokenHandler();

        string token = jwtHandler.WriteToken(jwtSecurityToken);
        return new TokenResponseDto
        {
            AccessToken = token,
            AccessTokenExpiration = accessTokenExpiration
        };
    }

    private async Task<IEnumerable<Claim>> GetClaims(User user, List<string> audiences)
    {
        var userList = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var roles = await _userManager.GetRolesAsync(user);
        if (roles.Count > 0)
        {
            userList.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));
        }

        userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

        return userList;
    }
    
}
