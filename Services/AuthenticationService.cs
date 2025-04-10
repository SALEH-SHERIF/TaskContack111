using Contact_management.Dtos;
using Contact_management.Models;
using Microsoft.AspNetCore.Identity;

namespace Contact_management.Services
{
	public class AuthenticationService:IAuthenticationService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IConfiguration _configuration;
		private readonly ITokenService _tokenService;

		public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration , ITokenService tokenService) 
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_configuration = configuration;
			_tokenService = tokenService;
		}

		#region RegisterAsync
		public async Task<AuthResponse> RegisterAsync(RegisterDto userDto)
		{
			var existingUser = await _userManager.FindByEmailAsync(userDto.Email);
			if (existingUser != null)
			{
				return new AuthResponse
				{
					Success = false,
					Message = "Email is already in use."
				};
			}

			var user = new ApplicationUser
			{
				UserName = userDto.userName,
				Email = userDto.Email
			};

			var result = await _userManager.CreateAsync(user, userDto.Password);

			if (!result.Succeeded)
			{
				return new AuthResponse
				{
					Success = false,
					Message = "Registration failed.",
					Errors = result.Errors.Select(e => e.Description).ToList() 
				};
			}

			return new AuthResponse
			{
				Success = true,
				Message = $"Registration successful for user with ID: {user.Id}"
			};
		}
		#endregion


		#region LoginAsync
		public async Task<AuthResponse> LoginAsync(LoginDto userDto)
		{
			var user = await _userManager.FindByEmailAsync(userDto.email);

			if (user == null || !(await _userManager.CheckPasswordAsync(user, userDto.password)))
			{
				return new AuthResponse
				{
					Success = false,
					Message = "Invalid email or password."
				};
			}

			var token = _tokenService.createToken(user);

			return new AuthResponse
			{
				Success = true,
				Message = "Login successful.",
				Token = token
			};
		}

		#endregion


	}
}
