using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebShopBackend.Models;

public class JwtService
{
	private readonly string _key;
	private readonly string _issuer;

	public JwtService(IConfiguration configuration)
	{
		_key = configuration["Jwt:Secret"] ?? throw new ArgumentNullException(nameof(configuration), "JWT Secret is missing in configuration.");
		_issuer = configuration["Jwt:Issuer"] ?? throw new ArgumentNullException(nameof(configuration), "JWT Issuer is missing in configuration.");
	}

	public string GenerateJwtToken(WebshopUser user)
	{
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id ?? throw new ArgumentNullException(nameof(user.Id), "User ID cannot be null.")),
			new Claim(ClaimTypes.Name, user.UserName ?? throw new ArgumentNullException(nameof(user.UserName), "Username cannot be null.")),
			new Claim(ClaimTypes.Email, user.Email ?? "")
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			_issuer,
			_issuer,
			claims,
			expires: DateTime.UtcNow.AddHours(2),
			signingCredentials: creds);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
