using Contact_management.Dtos;

namespace Contact_management.Services
{
	public interface IAuthenticationService
	{
		Task<AuthResponse> RegisterAsync(RegisterDto userDto);
		Task<AuthResponse> LoginAsync(LoginDto userDto);
	}
}
