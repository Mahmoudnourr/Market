using Market.Dtos;
using Market.Entities;
using Market.Services;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers.Auth
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : Controller
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("Register")]

		public async Task<IActionResult> RegistrationAsync(RegistrationDto registrationDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			else
			{
				Authmodel authModel = await _authService.RegisterAsync(registrationDto);
				if (!authModel.IsAuthenticated)
				{
					return BadRequest(authModel.message);
				}
				else
				{
					return Ok(authModel);
				}
			}
		}
		[HttpPost("Login")]
		public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var res = await _authService.GetTokenAsync(model);
			if (!res.IsAuthenticated)
			{
				return BadRequest(res.message);
			}
			return Ok(res);
		}
		[HttpGet]
		public IActionResult Test()
		{
			Authmodel s = new Authmodel();
			return Ok(s);
		}
		[HttpGet("Get")]
		public async Task<IActionResult> Getus(string email)
		{
			application_user us = await _authService.get(email);
			return Ok(us);
		}

	}

}
