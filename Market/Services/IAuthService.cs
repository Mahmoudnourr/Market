using Market.Dtos;

namespace Market.Services
{
	public interface IAuthService
	{
		Task<Authmodel> RegisterAsync(RegistrationDto registrationDto);

	}
}
