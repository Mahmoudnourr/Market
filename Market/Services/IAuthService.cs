using Market.Dtos;
using Market.Entities;

namespace Market.Services
{
	public interface IAuthService
	{
		Task<Authmodel> RegisterAsync(RegistrationDto registrationDto);
		Task<Authmodel> GetTokenAsync(TokenRequestModel model);
		Task<application_user> get(string email);
	}
}
