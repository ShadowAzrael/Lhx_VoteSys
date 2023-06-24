using Lhx_VoteSys.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Lhx_VoteSys.Models;
using Lhx_VoteSys.service;
/// <summary>
/// 认证处理服务
/// </summary>
public class JWTService : IJWTService
{
    private readonly JWTConfig _jwtConfig;

    public JWTService(IOptions<JWTConfig> jwtConfig)
    {
        this._jwtConfig = jwtConfig.Value;
    }

    public string CreateToken(string Email, string id)
    {
        //把有需要的信息写到Token
        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, id),
            new Claim(ClaimTypes.Email, Email),
        };

        //创建密钥
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
        //密钥加密
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //token配置
        var jwtToken = new JwtSecurityToken(_jwtConfig.Issuer,
            _jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(_jwtConfig.AccessExpiration),
            signingCredentials: credentials);

        //获取token
        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return token;
    }
}