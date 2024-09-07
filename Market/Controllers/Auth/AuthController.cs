using Market.Dtos;
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
	}
}
