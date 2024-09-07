﻿using Market.Dtos;
using Market.Entities;
using Market.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Market.Services
{
	public class Authservice : IAuthService
	{
		public readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly JWT _jwt;

		public Authservice(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, JWT jwt)
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

			ApplicationUser Customer = new ApplicationUser
			{
				UserName = registrationDto.UserName,
				Email = registrationDto.Email.ToLower(),
				FirstName = registrationDto.FirstName,
				LastName = registrationDto.LastName
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
				ExpiresOn = jwtSecurityToken.ValidTo,
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
			authModel.ExpiresOn = jwtSecurityToken.ValidTo;
			authModel.Roles = rolesList.ToList();
			authModel.FirstName = user.FirstName;
			authModel.LastName = user.LastName;
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

			return result.Succeeded ? string.Empty : "Sonething went wrong";
		}
		private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
		{
			var userClaims = await _userManager.GetClaimsAsync(user);
			var roles = await _userManager.GetRolesAsync(user);
			var roleClaims = new List<Claim>();

			foreach (var role in roles)
				roleClaims.Add(new Claim("roles", role));

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
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

		public async Task<ApplicationUser> get(string email)
		{
			ApplicationUser user = await _userManager.FindByEmailAsync(email);
			return user;
		}
	}
}
