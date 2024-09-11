using Market.Dtos;
using Market.Entities;
using Market.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Market.Services
{
	public class Authservice : IAuthService
	{
		public readonly UserManager<application_user> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly JWT _jwt;

		public Authservice(UserManager<application_user> userManager, RoleManager<IdentityRole> roleManager, JWT jwt)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_jwt = jwt;
		}
		public async Task<Authmodel> RegisterAsync(RegistrationDto registrationDto)
		{

			if (await _userManager.FindByEmailAsync(registrationDto.Email) is not null) { return new Authmodel { message = "The User Is Already registered" }; }
			if (await _userManager.FindByNameAsync(registrationDto.UserName) is not null)
			{
				return new Authmodel { message = "The UserName is Already Found" };

			}

			application_user Customer = new application_user
			{
				UserName = registrationDto.UserName,
				Email = registrationDto.Email.ToLower(),
				first_name = registrationDto.FirstName,
				last_name = registrationDto.LastName
			};
			var result = await _userManager.CreateAsync(Customer, registrationDto.Password);

			if (!result.Succeeded)
			{
				var errors = string.Empty;
				foreach (var error in result.Errors)
				{
					errors += $"{error.Description}\n";
				}
				return new Authmodel { message = errors };
			}
			await _userManager.AddToRoleAsync(Customer, "Customer");
			var jwtSecurityToken = await CreateJwtToken(Customer);

			return new Authmodel
			{
				Email = Customer.Email,
				FirstName = Customer.first_name,
				LastName = Customer.last_name,
			//	ExpiresOn = jwtSecurityToken.ValidTo,
				IsAuthenticated = true,
				Roles = new List<string> { "Customer" },
				Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
				Username = Customer.UserName
			};
		}
		public async Task<Authmodel> GetTokenAsync(TokenRequestModel model)
		{
			var authModel = new Authmodel();

			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
			{
				authModel.message = "Email or Password is incorrect!";
				return authModel;
			}

			var jwtSecurityToken = await CreateJwtToken(user);
			var rolesList = await _userManager.GetRolesAsync(user);

			authModel.IsAuthenticated = true;
			authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
			authModel.Email = user.Email;
			authModel.Username = user.UserName;
		//	authModel.ExpiresOn = jwtSecurityToken.ValidTo;
			authModel.Roles = rolesList.ToList();
			authModel.FirstName = user.first_name;
			authModel.LastName = user.last_name;
            if(user.refresh_tokens.Any(t=>t.is_active))
             {
				var activeRefreshToken = user.refresh_tokens.FirstOrDefault(t=>t.is_active);
				authModel.RefreshToken = activeRefreshToken.token;
				authModel.RefreshTokenExpiration = activeRefreshToken.expires_on;
			 }
			 else
			 {
				var refreshToken = GenerateRefreshTokenAsync();
				authModel.RefreshToken = refreshToken.token;
				authModel.RefreshTokenExpiration = refreshToken.expires_on;
				user.refresh_tokens.Add(refreshToken);
				await _userManager.UpdateAsync(user);
			 }

			return authModel;
		}
		public async Task<string> AddRoleAsync(AddRoleModel model)
		{
			var user = await _userManager.FindByIdAsync(model.UserId);

			if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
				return "Invalid user ID or Role";

			if (await _userManager.IsInRoleAsync(user, model.Role))
				return "User already assigned to this role";

			var result = await _userManager.AddToRoleAsync(user, model.Role);

			return result.Succeeded ? string.Empty : "Something went wrong";
		}

		private async Task<JwtSecurityToken> CreateJwtToken(application_user user)
		{
			var userClaims = await _userManager.GetClaimsAsync(user);
			var roles = await _userManager.GetRolesAsync(user);
			var roleClaims = new List<Claim>();

			foreach (var role in roles)
				roleClaims.Add(new Claim("roles", role));

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim("uid", user.Id)
			}
			.Union(userClaims)
			.Union(roleClaims);

			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
			var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

			var jwtSecurityToken = new JwtSecurityToken(
				issuer: _jwt.Issuer,
				audience: _jwt.Audience,
				claims: claims,
				expires: DateTime.Now.AddDays(_jwt.DurationInDays),
				signingCredentials: signingCredentials);

			return jwtSecurityToken;
		}

		public async Task<application_user> get(string email)
		{
			application_user user = await _userManager.FindByEmailAsync(email);
			return user;

		}
		private refresh_token GenerateRefreshTokenAsync ()
		{
             var randomnumber = new byte[32];
			 using var rng = new RNGCryptoServiceProvider();
			 rng.GetBytes(randomnumber);
			 return new refresh_token
			 {
				token = Convert.ToBase64String(randomnumber),
				expires_on = DateTime.Now.AddDays(10),
				created_on= DateTime.Now
			 };
		}
	}
}
