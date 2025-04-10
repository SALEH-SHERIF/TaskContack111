using Contact_management.Dtos;
using Contact_management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contact_management.Controllers
{
	
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthenticationService _authenticationService;

		public AuthenticationController(IAuthenticationService authenticationService)
		{
			_authenticationService = authenticationService;
		}

		#region Register
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
		{
			var result = await _authenticationService.RegisterAsync(registerDto);

			if (!result.Success)
			{
				return BadRequest(result); 
			}

			return Ok(result);
		}
		#endregion

		#region Login
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
		{
		
			//if (!ModelState.IsValid)
			//{
			//	return BadRequest(ModelState);
			//}

			var result = await _authenticationService.LoginAsync(loginDto);

			if (!result.Success)
			{
				return Unauthorized(result); 
			}

			return Ok(new { token = result.Token });
		}

		#endregion
	}
}
