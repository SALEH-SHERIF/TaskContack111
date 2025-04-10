using Contact_management.Models;

namespace Contact_management.Services
{
	public interface ITokenService
	{
		string createToken(ApplicationUser user);
	}
}
